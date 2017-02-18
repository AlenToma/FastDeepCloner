using System;
using System.Collections;
using System.Collections.Generic;

namespace FastDeepCloner
{
    internal class ClonerShared
    {
        private Dictionary<ClonedItems, object> _alreadyCloned = new Dictionary<ClonedItems, object>();
        private FastDeepClonerSettings _settings;

        internal ClonerShared(FieldType fieldType)
        {
            _settings = new FastDeepClonerSettings() { FieldType = fieldType };

        }

        internal ClonerShared(FastDeepClonerSettings settings)
        {
            _settings = settings;
        }

        private object ReferenceTypeClone(List<IFastDeepClonerProperty> properties, Type _primaryType, object objectToBeCloned, object appendToValue = null)
        {

            var resObject = appendToValue == null ? _settings.OnCreateInstance(_primaryType) : appendToValue;
            var fullPath = _primaryType.Name;
            foreach (IFastDeepClonerProperty property in properties)
            {
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

                if (property.IsInternalType)
                    property.SetValue(resObject, value);
                else
                {
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
            Type _primaryType = objectToBeCloned.GetType();
            if (_primaryType.IsArray && _primaryType.GetArrayRank() > 1)
                return ((Array)objectToBeCloned).Clone();

            Object resObject;
            if (_primaryType.IsArray || (objectToBeCloned as IList) != null)
            {
                resObject = _primaryType.IsArray ? Array.CreateInstance(_primaryType.GetIListType(), (objectToBeCloned as Array).Length) : Activator.CreateInstance(_primaryType.GetIListType());
                var i = 0;
                var ilist = resObject as IList;
                var array = resObject as Array;
                foreach (var item in (objectToBeCloned as IList))
                {
                    object clonedIteam = null;
                    if (item != null)
                    {
                        clonedIteam = item.GetType().IsInternalType()
                            ? item
                            : Clone(item);
                    }
                    if (!_primaryType.IsArray)
                        ilist.Add(clonedIteam);
                    else
                        array.SetValue(clonedIteam, i);

                    i++;
                }
            }
            else if (objectToBeCloned is IDictionary)
            {
                resObject = _settings.OnCreateInstance(_primaryType);
                var resDic = resObject as IDictionary;
                var dictionary = (IDictionary)objectToBeCloned;
                foreach (var key in dictionary.Keys)
                {
                    var item = dictionary[key];
                    object clonedIteam = null;
                    if (item != null)
                    {
                        clonedIteam = item.GetType().IsInternalType()
                            ? item
                            : Clone(item);
                    }
                    resDic.Add(key, clonedIteam);
                }
            }
            else
            {
                resObject = ReferenceTypeClone((_settings.FieldType == FieldType.FieldInfo ? _primaryType.GetFastDeepClonerFields() : _primaryType.GetFastDeepClonerProperties()), _primaryType, objectToBeCloned);
                if (_settings.FieldType == FieldType.Both)
                    resObject = ReferenceTypeClone(_primaryType.GetFastDeepClonerFields().FindAll(x => !_primaryType.GetFastDeepClonerProperties().Exists(a => a.Name == x.Name)), _primaryType, objectToBeCloned, resObject);
            }

            return resObject;
        }

    }
}
