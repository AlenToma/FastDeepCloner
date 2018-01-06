using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FastDeepCloner
{
    internal class ClonerShared
    {
        private readonly Dictionary<ClonedItems, object> _alreadyCloned = new Dictionary<ClonedItems, object>();
        private readonly FastDeepClonerSettings _settings;

        internal ClonerShared(FieldType fieldType)
        {
            _settings = new FastDeepClonerSettings() { FieldType = fieldType };

        }

        internal ClonerShared(FastDeepClonerSettings settings)
        {
            _settings = settings;
        }

        private object ReferenceTypeClone(Dictionary<string, IFastDeepClonerProperty> properties, Type primaryType, object objectToBeCloned, object appendToValue = null)
        {

            var resObject = appendToValue ?? _settings.OnCreateInstance(primaryType);
            var fullPath = primaryType.Name;
            foreach (var property in properties.Values)
            {
                if (!property.CanRead || property.FastDeepClonerIgnore)
                    continue;
                var value = property.GetValue(objectToBeCloned);
                if (value == null)
                    continue;
                var key = fullPath + property.Name;
                var clonedItem = new ClonedItems() { Value = value, Key = key };
                if (_alreadyCloned.ContainsKey(clonedItem))
                {
                    property.SetValue(resObject, _alreadyCloned[clonedItem]);
                    continue;
                }

                if (property.IsInternalType || value.GetType().IsInternalType())
                    property.SetValue(resObject, value);
                else
                {
                    if (_settings.CloneLevel == CloneLevel.FirstLevelOnly)
                        continue;
                    var tValue = Clone(value);
                    _alreadyCloned.Add(clonedItem, tValue);
                    property.SetValue(resObject, tValue);
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
            else
            {
                resObject = ReferenceTypeClone((_settings.FieldType == FieldType.FieldInfo ? primaryType.GetFastDeepClonerFields() : primaryType.GetFastDeepClonerProperties()), primaryType, objectToBeCloned);
                if (_settings.FieldType == FieldType.Both)
                    resObject = ReferenceTypeClone(primaryType.GetFastDeepClonerFields().Values.ToList().Where(x => !primaryType.GetFastDeepClonerProperties().ContainsKey(x.Name)).ToDictionary(x => x.Name, x => x), primaryType, objectToBeCloned, resObject);
            }

            return resObject;
        }

    }
}
