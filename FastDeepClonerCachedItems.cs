using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FastDeepCloner
{
    internal static class FastDeepClonerCachedItems
    {

        private static Dictionary<Type, FieldInfo[]> _cachedFields = new Dictionary<Type, FieldInfo[]>();
        private static Dictionary<Type, PropertyInfo[]> _cachedPropertyInfo = new Dictionary<Type, PropertyInfo[]>();
        private static Dictionary<Type, Type> _cachedTypes = new Dictionary<Type, Type>();

        internal static Type GetIListType(this Type type)
        {
            if (_cachedTypes.ContainsKey(type))
                return _cachedTypes[type];
            if (type.IsArray)
                _cachedTypes.Add(type, type.GetElementType());
            else
                _cachedTypes.Add(type, typeof(List<>).MakeGenericType(type.GenericTypeArguments.First()));

            return _cachedTypes[type];
        }


        internal static FieldInfo[] GetFastDeepClonerFields(this Type primaryType)
        {
            if (!_cachedFields.ContainsKey(primaryType))
            {
                var properties = new List<FieldInfo>();
                if (primaryType.GetTypeInfo().BaseType != null && primaryType.GetTypeInfo().BaseType.Name != "Object")
                {
                    properties.AddRange(primaryType.GetTypeInfo().BaseType.GetRuntimeFields().ToList());
                    properties.AddRange(primaryType.GetRuntimeFields().Where(x => !properties.Any(p => p.Name == x.Name)));
                }
                else properties.AddRange(primaryType.GetRuntimeFields().ToList());

                properties.RemoveAll(x => x.GetCustomAttribute<FastDeepClonerIgnore>() != null);
                _cachedFields.Add(primaryType, properties.ToArray());
            }



            return _cachedFields[primaryType];
        }


        internal static PropertyInfo[] GetFastDeepClonerProperties(this Type primaryType)
        {

            if (!_cachedPropertyInfo.ContainsKey(primaryType))
            {
                var properties = new List<PropertyInfo>();
                if (primaryType.GetTypeInfo().BaseType != null && primaryType.GetTypeInfo().BaseType.Name != "Object")
                {
                    properties.AddRange(primaryType.GetTypeInfo().BaseType.GetRuntimeProperties().ToList());
                    properties.AddRange(primaryType.GetRuntimeProperties().Where(x => !properties.Any(p => p.Name == x.Name)));
                }
                else properties.AddRange(primaryType.GetRuntimeProperties().ToList());

                properties.RemoveAll(x => x.GetCustomAttribute<FastDeepClonerIgnore>() != null);
                _cachedPropertyInfo.Add(primaryType, properties.ToArray());
            }


            return _cachedPropertyInfo[primaryType];
        }

    }
}
