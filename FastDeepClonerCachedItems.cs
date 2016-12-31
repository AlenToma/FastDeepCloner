using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FastDeepCloner
{
    internal static class FastDeepClonerCachedItems
    {

        private static Dictionary<Type, List<FieldInfo>> _cachedFields = new Dictionary<Type, List<FieldInfo>>();
        private static Dictionary<Type, List<PropertyInfo>> _cachedPropertyInfo = new Dictionary<Type, List<PropertyInfo>>();


        internal static List<FieldInfo> GetFastDeepClonerFields(this Type primaryType)
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

                _cachedFields.Add(primaryType, properties);
                _cachedFields[primaryType].RemoveAll(x => x.GetCustomAttribute<FastDeepClonerIgnore>() != null);
            }



            return _cachedFields[primaryType];
        }


        internal static List<PropertyInfo> GetFastDeepClonerProperties(this Type primaryType)
        {

            if (!_cachedPropertyInfo.ContainsKey(primaryType))
            {
                var properties = new List<PropertyInfo>();
                if (primaryType.GetTypeInfo().BaseType != null && primaryType.GetTypeInfo().BaseType.Name != "Object")
                {
                    properties.AddRange(primaryType.GetTypeInfo().BaseType.GetRuntimeProperties().ToList());
                    properties.AddRange(primaryType.GetRuntimeProperties().Where(x=> !properties.Any(p=> p.Name == x.Name)));
                }
                else properties.AddRange(primaryType.GetRuntimeProperties().ToList());

                _cachedPropertyInfo.Add(primaryType, properties);
                _cachedPropertyInfo[primaryType].RemoveAll(x => x.GetCustomAttribute<FastDeepClonerIgnore>() != null);
            }


            return _cachedPropertyInfo[primaryType];
        }

    }
}
