using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FastDeepCloner
{
    internal class ReferenceClone
    {
        private readonly SafeValueType<string, object> _alreadyCloned = new SafeValueType<string, object>();
        private readonly FastDeepClonerSettings _settings;

        internal ReferenceClone(FieldType fieldType)
        {
            _settings = new FastDeepClonerSettings() { FieldType = fieldType };

        }

        internal ReferenceClone(FastDeepClonerSettings settings)
        {
            if (settings != null)
                _settings = settings;
            else _settings = new FastDeepClonerSettings() { FieldType = FieldType.PropertyInfo };
        }

        internal object CloneTo(object itemToClone, object CloneToItem)
        {
            var identifier = CloneToItem.GetFastDeepClonerIdentifier();
            if (identifier != null && _alreadyCloned.ContainsKey(identifier))
                return _alreadyCloned[identifier];

            if (identifier != null)
                _alreadyCloned.Add(identifier, CloneToItem);

            var type2 = CloneToItem.GetType();
            var props2 = FastDeepClonerCachedItems.GetFastDeepClonerProperties(type2);
            foreach (var prop2 in props2)
            {
                if (!prop2.Value.CanWrite)
                    continue;
                var item = itemToClone;
                var prop = item.GetType().GetProperty(prop2.Key);
                foreach (var attr in prop2.Value.GetCustomAttributes<FastDeepClonerColumn>())
                {
                    var strSplit = attr.ColumnName.Split('.');
                    for (var i = 0; i < strSplit.Length; i++)
                    {
                        var p = item.GetType().GetProperty(strSplit[i]);
                        if (p != null)
                        {
                            prop = p;

                            if (i != strSplit.Length - 1)
                                item = p.GetValue(item);
                            else
                                break;
                        }
                    }

                }

                if (prop != null)
                {
                    var value = prop.GetValue(item);
                    if (value == null)
                        continue;
                    if (prop.PropertyType.IsInternalType())
                    {
                        prop2.Value.SetValue(CloneToItem, FastDeepClonerCachedItems.Value(value, prop2.Value.PropertyType, true));
                    }
                    else if (prop.PropertyType == prop2.Value.PropertyType)
                        prop2.Value.SetValue(CloneToItem, value.Clone());
                    else if (prop.PropertyType.GetIListType() == prop.PropertyType && prop2.Value.PropertyType.GetIListType() == prop2.Value.PropertyType) // if not list
                    {
                        var value2 = prop2.Value.GetValue(item);
                        if (value2 == null)
                            value2 = prop2.Value.PropertyType.CreateInstance();
                        prop2.Value.SetValue(CloneToItem, CloneTo(value, value2));
                    }

                }

            }

            return CloneToItem;
        }

        private object ReferenceTypeClone(Dictionary<string, IFastDeepClonerProperty> properties, Type primaryType, object objectToBeCloned, object appendToValue = null)
        {
            var identifier = objectToBeCloned.GetFastDeepClonerIdentifier();
            if (identifier != null && _alreadyCloned.ContainsKey(identifier))
                return _alreadyCloned[identifier];

            var resObject = appendToValue ?? _settings.OnCreateInstance(primaryType);

            if (identifier != null)
                _alreadyCloned.Add(identifier, resObject);

            foreach (var property in properties.Values)
            {
                if (!property.CanRead || property.FastDeepClonerIgnore)
                    continue;
                var value = property.GetValue(objectToBeCloned);
                if (value == null)
                    continue;

                if (property.NoneCloneable || property.IsInternalType || value.GetType().IsInternalType())
                    property.SetValue(resObject, value);
                else
                {
                    if (_settings.CloneLevel == CloneLevel.FirstLevelOnly)
                        continue;
                    property.SetValue(resObject, Clone(value));
                }
            }

            return resObject;
        }
        internal object Clone(object objectToBeCloned)
        {
            if (objectToBeCloned == null)
                return null;
            var primaryType = objectToBeCloned.GetType();
            if (primaryType.IsArray && primaryType.GetArrayRank() > 1)
                return ((Array)objectToBeCloned).Clone();

            if (objectToBeCloned.IsInternalObject())
                return objectToBeCloned;

            object resObject;
            if (primaryType.IsArray || (objectToBeCloned as IList) != null)
            {
                resObject = primaryType.IsArray ? Array.CreateInstance(primaryType.GetIListType(), (objectToBeCloned as Array).Length) : Activator.CreateInstance(primaryType.GetIListType());
                var i = 0;
                var ilist = resObject as IList;
                var array = resObject as Array;

                foreach (var item in (objectToBeCloned as IList))
                {
                    object clonedIteam = null;
                    if (item != null)
                    {
                        clonedIteam = item.GetType().IsInternalType() ? item : Clone(item);
                    }
                    if (!primaryType.IsArray)
                        ilist?.Add(clonedIteam);
                    else
                        array?.SetValue(clonedIteam, i);
                    i++;
                }

                foreach (var prop in FastDeepClonerCachedItems.GetFastDeepClonerProperties(primaryType).Where(x => !FastDeepClonerCachedItems.GetFastDeepClonerProperties(typeof(List<string>)).Any(a => a.Key == x.Key)))
                {
                    var property = prop.Value;
                    if (!property.CanRead || property.FastDeepClonerIgnore)
                        continue;
                    var value = property.GetValue(objectToBeCloned);
                    if (value == null)
                        continue;
                    var clonedIteam = value.GetType().IsInternalType() ? value : Clone(value);
                    property.SetValue(resObject, clonedIteam);
                }
            }
            else if (objectToBeCloned is IDictionary)
            {
                resObject = Activator.CreateInstance(primaryType);
                var resDic = resObject as IDictionary;
                var dictionary = (IDictionary)objectToBeCloned;
                foreach (var key in dictionary.Keys)
                {
                    var item = dictionary[key];
                    object clonedIteam = null;
                    if (item != null)
                    {
                        clonedIteam = item.GetType().IsInternalType() ? item : Clone(item);
                    }
                    resDic?.Add(key, clonedIteam);
                }
            }
            else if (primaryType.IsAnonymousType()) // dynamic types
            {

                var props = FastDeepClonerCachedItems.GetFastDeepClonerProperties(primaryType);
                resObject = new ExpandoObject();
                var d = resObject as IDictionary<string, object>;
                foreach (var prop in props.Values)
                {
                    var item = prop.GetValue(objectToBeCloned);
                    var value = item == null || prop.IsInternalType || (item?.IsInternalObject() ?? true) ? item : Clone(item);
                    if (!d.ContainsKey(prop.Name))
                        d.Add(prop.Name, value);
                }
            }
            else
            {
                resObject = ReferenceTypeClone((_settings.FieldType == FieldType.FieldInfo ? FastDeepClonerCachedItems.GetFastDeepClonerFields(primaryType) : FastDeepClonerCachedItems.GetFastDeepClonerProperties(primaryType)), primaryType, objectToBeCloned);
                if (_settings.FieldType == FieldType.Both)
                    resObject = ReferenceTypeClone(FastDeepClonerCachedItems.GetFastDeepClonerFields(primaryType).Values.ToList().Where(x => !FastDeepClonerCachedItems.GetFastDeepClonerProperties(primaryType).ContainsKey(x.Name)).ToDictionary(x => x.Name, x => x), primaryType, objectToBeCloned, resObject);
            }

            return resObject;
        }

    }
}
