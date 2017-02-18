using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FastDeepCloner
{
    internal static class FastDeepClonerCachedItems
    {
        private static Dictionary<Type, List<IFastDeepClonerProperty>> _cachedFields = new Dictionary<Type, List<IFastDeepClonerProperty>>();
        private static Dictionary<Type, List<IFastDeepClonerProperty>> _cachedPropertyInfo = new Dictionary<Type, List<IFastDeepClonerProperty>>();
        private static Dictionary<Type, Type> _cachedTypes = new Dictionary<Type, Type>();
        private static Dictionary<Type, Func<object>> _cachedConstructor = new Dictionary<Type, Func<object>>();
        internal static Type GetIListType(this Type type)
        {
            if (_cachedTypes.ContainsKey(type))
                return _cachedTypes[type];
            if (type.IsArray)
                _cachedTypes.Add(type, type.GetElementType());
            else
            {
                _cachedTypes.Add(type, typeof(List<>).MakeGenericType(type.GenericTypeArguments.First()));
            }
            return _cachedTypes[type];
        }

        internal static object Creator(this Type type)
        {
            if (_cachedConstructor.ContainsKey(type))
                return _cachedConstructor[type].Invoke();
            _cachedConstructor.Add(type, Expression.Lambda<Func<object>>(Expression.New(type)).Compile());
            return _cachedConstructor[type].Invoke();
        }

        internal static bool GetField(this FieldInfo field, List<IFastDeepClonerProperty> properties)
        {
            var f = new FastDeepClonerProperty(field);
            if (f.CanRead)
                properties.Add(f);
            return true;
        }

        internal static bool GetField(this PropertyInfo field, List<IFastDeepClonerProperty> properties)
        {
            var f = new FastDeepClonerProperty(field);
            if (f.CanRead)
                properties.Add(f);
            return true;
        }

        internal static List<IFastDeepClonerProperty> GetFastDeepClonerFields(this Type primaryType)
        {
            if (!_cachedFields.ContainsKey(primaryType))
            {
                var properties = new List<IFastDeepClonerProperty>();
                if (primaryType.GetTypeInfo().BaseType != null && primaryType.GetTypeInfo().BaseType.Name != "Object")
                {
                    primaryType.GetTypeInfo().BaseType.GetRuntimeFields().Where(x => x.GetField(properties)).ToList();
                    primaryType.GetRuntimeFields().Where(x => x.GetField(properties)).ToList();
                }
                else primaryType.GetRuntimeFields().Where(x => x.GetField(properties)).ToList();
                _cachedFields.Add(primaryType, properties);
            }
            return _cachedFields[primaryType];
        }


        internal static List<IFastDeepClonerProperty> GetFastDeepClonerProperties(this Type primaryType)
        {
            if (!_cachedPropertyInfo.ContainsKey(primaryType))
            {
                var properties = new List<IFastDeepClonerProperty>();
                if (primaryType.GetTypeInfo().BaseType != null && primaryType.GetTypeInfo().BaseType.Name != "Object")
                {
                    primaryType.GetTypeInfo().BaseType.GetRuntimeProperties().Where(x => x.GetField(properties)).ToList();
                    primaryType.GetRuntimeProperties().Where(x => x.GetField(properties)).ToList();
                }
                else primaryType.GetRuntimeProperties().Where(x => x.GetField(properties)).ToList();
                _cachedPropertyInfo.Add(primaryType, properties);
            }
            return _cachedPropertyInfo[primaryType];
        }
    }
}
