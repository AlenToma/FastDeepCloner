using System;
using System.Collections.Generic;
using System.Linq;

namespace FastDeepCloner
{
    /// <summary>
    /// DeepCloner
    /// </summary>
    public static class DeepCloner
    {
        /// <summary>
        /// Clear cached data
        /// </summary>
        public static void CleanCachedItems()
        {
            FastDeepClonerCachedItems.CleanCachedItems();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T Clone<T>(this T objectToBeCloned, FastDeepClonerSettings settings) where T : class
        {
            return (T)new ClonerShared(settings).Clone(objectToBeCloned);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="fieldType">Clone Method</param>
        /// <returns></returns>
        public static object Clone(this object objectToBeCloned, FieldType fieldType = FieldType.PropertyInfo)
        {
            return new ClonerShared(fieldType).Clone(objectToBeCloned);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="fieldType">Clone Method</param>
        /// <returns></returns>
        public static T Clone<T>(this T objectToBeCloned, FieldType fieldType = FieldType.PropertyInfo) where T : class
        {
            return (T)new ClonerShared(fieldType).Clone(objectToBeCloned);
        }

        /// <summary>
        /// Create CreateInstance()
        /// this will use ILGenerator to create new object from the cached ILGenerator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>() where T : class
        {
            return (T)typeof(T).Creator();
        }

        /// <summary>
        /// Create CreateInstance()
        /// this will use ILGenerator to create new object from the cached ILGenerator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static object CreateInstance(this Type type)
        {
            return type.Creator();
        }

        /// <summary>
        /// will return fieldInfo from the cached fieldinfo. Get and set value is much faster.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<IFastDeepClonerProperty> GetFastDeepClonerFields(this Type type)
        {
            return FastDeepClonerCachedItems.GetFastDeepClonerFields(type).Values.ToList();
        }

        /// <summary>
        /// will return propertyInfo from the cached propertyInfo. Get and set value is much faster.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<IFastDeepClonerProperty> GetFastDeepClonerProperties(this Type type)
        {
            return FastDeepClonerCachedItems.GetFastDeepClonerProperties(type).Values.ToList();
        }

        /// <summary>
        /// Get fild by Name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IFastDeepClonerProperty GetField(this Type type, string name)
        {
            return FastDeepClonerCachedItems.GetFastDeepClonerFields(type).ContainsKey(name)
                ? FastDeepClonerCachedItems.GetFastDeepClonerFields(type)[name]
                : null;
        }

        /// <summary>
        /// Get Property by name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IFastDeepClonerProperty GetProperty(this Type type, string name)
        {
            return FastDeepClonerCachedItems.GetFastDeepClonerProperties(type).ContainsKey(name)
                ? FastDeepClonerCachedItems.GetFastDeepClonerProperties(type)[name]
                : null;
        }
    }
}
