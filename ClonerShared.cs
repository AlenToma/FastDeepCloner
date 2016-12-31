using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FastDeepCloner
{
    internal class ClonerShared
    {
        private FieldType _fieldtype;
        private object _objectToBeCloned;
        private Type _primaryType;
        private Dictionary<object, object> _alreadyCloned;
        internal ClonerShared(object objectToBeCloned, FieldType fieldType = FieldType.PropertyInfo)
        {
            _fieldtype = fieldType;
            _objectToBeCloned = objectToBeCloned;
            _primaryType = objectToBeCloned?.GetType() ?? null;
            _alreadyCloned = new Dictionary<object, object>();
        }

        private ClonerShared(object objectToBeCloned, FieldType fieldType, Dictionary<object, object> alreadyCloned)
        {
            _fieldtype = fieldType;
            _objectToBeCloned = objectToBeCloned;
            _primaryType = objectToBeCloned?.GetType() ?? null;
            _alreadyCloned = alreadyCloned;
        }

        /// <summary>
        /// Determines if the specified type is an internal type.
        /// </summary>
        /// <param name="underlyingSystemType"></param>
        /// <returns><c>true</c> if type is internal, else <c>false</c>.</returns>
        private static bool IsInternalType(Type underlyingSystemType)
        {
            return underlyingSystemType == typeof(string) ||
                underlyingSystemType == typeof(decimal) ||
                underlyingSystemType == typeof(int) ||
                underlyingSystemType == typeof(double) ||
                underlyingSystemType == typeof(float) ||
                underlyingSystemType == typeof(bool) ||
                underlyingSystemType == typeof(long) ||
                underlyingSystemType == typeof(DateTime) ||
                underlyingSystemType == typeof(ushort) ||
                underlyingSystemType == typeof(short) ||
                underlyingSystemType == typeof(sbyte) ||
                underlyingSystemType == typeof(byte) ||
                underlyingSystemType == typeof(ulong) ||
                underlyingSystemType == typeof(uint) ||
                underlyingSystemType == typeof(char) ||
                underlyingSystemType == typeof(TimeSpan) ||
                underlyingSystemType == typeof(decimal?) ||
                underlyingSystemType == typeof(int?) ||
                underlyingSystemType == typeof(double?) ||
                underlyingSystemType == typeof(float?) ||
                underlyingSystemType == typeof(bool?) ||
                underlyingSystemType == typeof(long?) ||
                underlyingSystemType == typeof(DateTime?) ||
                underlyingSystemType == typeof(ushort?) ||
                underlyingSystemType == typeof(short?) ||
                underlyingSystemType == typeof(sbyte?) ||
                underlyingSystemType == typeof(byte?) ||
                underlyingSystemType == typeof(ulong?) ||
                underlyingSystemType == typeof(uint?) ||
                underlyingSystemType == typeof(char?) ||
                underlyingSystemType == typeof(TimeSpan?);
        }

        internal object Clone()
        {
            if (_objectToBeCloned == null)
                return null;

            if (_primaryType.IsArray && _primaryType.GetArrayRank() > 1)
                return ((Array)_objectToBeCloned).Clone();

            Object resObject;

            if (_primaryType.IsArray || (_objectToBeCloned as IList) != null)
            {
                if ((_objectToBeCloned as IList) != null && (_objectToBeCloned as IList).Count <= 0)
                {
                    return Activator.CreateInstance(_primaryType);
                }
                resObject = _primaryType.IsArray ? Array.CreateInstance(_primaryType.GetElementType(), (_objectToBeCloned as Array).Length) : Activator.CreateInstance(typeof(List<>).MakeGenericType((_objectToBeCloned as IList)[0].GetType()));
                var i = 0;
                foreach (var item in (_objectToBeCloned as IList))
                {
                    object clonedIteam = null;
                    if (item != null)
                    {
                        var underlyingSystemType = item.GetType();
                        clonedIteam = (item is string || IsInternalType(underlyingSystemType))
                            ? item
                            : new ClonerShared(item, _fieldtype, _alreadyCloned).Clone();
                    }
                    if (!_primaryType.IsArray)
                        ((IList)resObject).Add(clonedIteam);
                    else
                        ((Array)resObject).SetValue(clonedIteam, i);

                    i++;
                }
            }
            else if (_objectToBeCloned is IDictionary)
            {

                resObject = Activator.CreateInstance(_primaryType);
                var dictionary = (IDictionary)_objectToBeCloned;
                foreach (var key in dictionary.Keys)
                {
                    var item = dictionary[key];
                    object clonedIteam = null;
                    if (item != null)
                    {
                        var underlyingSystemType = item.GetType();
                        clonedIteam = (item is string || IsInternalType(underlyingSystemType))
                            ? item
                            : new ClonerShared(item, _fieldtype, _alreadyCloned).Clone();
                    }
                    ((IDictionary)resObject).Add(key, clonedIteam);
                }
            }
            else
            {
                resObject = Activator.CreateInstance(_primaryType);
                var fullPath = _primaryType.Name;
                if (_fieldtype == FieldType.FieldInfo)
                {
                    foreach (var property in _primaryType.GetFastDeepClonerFields())
                    {
                        // Validate if the property is a writable one.
                        if (property.IsInitOnly || property.FieldType == typeof(System.IntPtr))
                            continue;

                        var value = property.GetValue(_objectToBeCloned);
                        if (value == null)
                            continue;

                        if (_alreadyCloned.ContainsKey(value) && _alreadyCloned.ContainsValue(fullPath + property.Name))
                        {
                            property.SetValue(resObject, _alreadyCloned[value]);
                            continue;
                        }


                        if ((value is string || IsInternalType(property.FieldType)))
                            property.SetValue(resObject, value);
                        else
                        {
                            var tValue = new ClonerShared(value, _fieldtype, _alreadyCloned).Clone();
                            _alreadyCloned.Add(value, fullPath + property.Name);
                            property.SetValue(resObject, tValue);
                        }
                    }
                }
                else
                {
                    foreach (var property in _primaryType.GetFastDeepClonerProperties())
                    {
                        // Validate if the property is a writable one.
                        if (!property.CanWrite || !property.CanRead || property.PropertyType == typeof(System.IntPtr))
                            continue;
                        var value = property.GetValue(_objectToBeCloned);
                        if (value == null)
                            continue;

                        if (_alreadyCloned.ContainsKey(value) && _alreadyCloned.ContainsValue(fullPath + property.Name))
                        {
                            property.SetValue(resObject, _alreadyCloned[value]);
                            continue;
                        }


                        if ((value is string || IsInternalType(property.PropertyType)))
                            property.SetValue(resObject, value);
                        else
                        {
                            var tValue = new ClonerShared(value, _fieldtype, _alreadyCloned).Clone();
                            _alreadyCloned.Add(value, fullPath + property.Name);
                            property.SetValue(resObject, tValue);
                        }
                    }
                }
            }

            return resObject;
        }

    }
}
