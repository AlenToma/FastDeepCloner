using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FastDeepCloner
{
    internal class ClonerShared
    {
        internal object Clone(object objectToBeCloned, FieldType fieldType, Dictionary<ClonedItems, object> alreadyCloned = null)
        {
            if (objectToBeCloned == null)
                return null;
            Type _primaryType = objectToBeCloned.GetType();
            Dictionary<ClonedItems, object> _alreadyCloned = alreadyCloned ?? new Dictionary<ClonedItems, object>();

            if (_primaryType.IsArray && _primaryType.GetArrayRank() > 1)
                return ((Array)objectToBeCloned).Clone();

            Object resObject;
            if (_primaryType.IsArray || (objectToBeCloned as IList) != null)
            {
                resObject = _primaryType.IsArray ? Array.CreateInstance(_primaryType.GetIListType(), (objectToBeCloned as Array).Length) : Activator.CreateInstance(_primaryType.GetIListType());
                var i = 0;
                foreach (var item in (objectToBeCloned as IList))
                {
                    object clonedIteam = null;
                    if (item != null)
                    {
                        clonedIteam = item.GetType().IsInternalType()
                            ? item
                            : Clone(item, fieldType, _alreadyCloned);
                    }
                    if (!_primaryType.IsArray)
                        ((IList)resObject).Add(clonedIteam);
                    else
                        ((Array)resObject).SetValue(clonedIteam, i);

                    i++;
                }
            }
            else if (objectToBeCloned is IDictionary)
            {

                resObject = Activator.CreateInstance(_primaryType);
                var dictionary = (IDictionary)objectToBeCloned;
                foreach (var key in dictionary.Keys)
                {
                    var item = dictionary[key];
                    object clonedIteam = null;
                    if (item != null)
                    {
                        clonedIteam = item.GetType().IsInternalType()
                            ? item
                            : Clone(item, fieldType, _alreadyCloned);
                    }
                    ((IDictionary)resObject).Add(key, clonedIteam);
                }
            }
            else
            {
                resObject = Activator.CreateInstance(_primaryType);
                var fullPath = _primaryType.Name;
                foreach (IFastDeepClonerProperty property in (fieldType == FieldType.FieldInfo ? _primaryType.GetFastDeepClonerFields() : _primaryType.GetFastDeepClonerProperties()))
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
                        var tValue = Clone(value, fieldType, _alreadyCloned);
                        _alreadyCloned.Add(clonedItem, tValue);
                        property.SetValue(resObject, tValue);
                    }
                }
            }

            return resObject;
        }

    }
}
