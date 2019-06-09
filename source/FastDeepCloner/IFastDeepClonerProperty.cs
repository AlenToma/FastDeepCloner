using System;
using System.Reflection;

namespace FastDeepCloner
{
    /// <summary>
    /// Interface for FastDeepClonerProperty
    /// </summary>
    public interface IFastDeepClonerProperty
    {
        /// <summary>
        /// Get Method for GetValue()
        /// </summary>
        Func<object, object> GetMethod { get; set; }

        /// <summary>
        /// Set Method for SetValue()
        /// </summary>
        Action<object, object> SetMethod { get; set; }

        /// <summary>
        /// PropertyName
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Property FullName
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Set Value
        /// </summary>
        /// <param name="o"></param>
        /// <param name="value"></param>
        void SetValue(object o, object value);

        /// <summary>
        /// Get Value
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        object GetValue(object o);

        /// <summary>
        /// CanRead= !(field.IsInitOnly || field.FieldType == typeof(IntPtr) || field.IsLiteral);
        /// or for PropertyInfo
        /// CanRead= !(!property.CanWrite || !property.CanRead || property.PropertyType == typeof(IntPtr) || property.GetIndexParameters().Length > 0);
        /// </summary>
        // able to process or clone
        bool CanRead { get; }

        /// <summary>
        /// If you could write to the propertyInfo
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        /// Simple can read. this should have been called CanRead to bad we alread have CanRead Property, its a pain to change it now.
        /// </summary>
        bool ReadAble { get; }

        /// <summary>
        /// Ignored
        /// </summary>
        // Have attr FastDeepClonerIgnore
        bool FastDeepClonerIgnore { get; }

        /// <summary>
        /// All available attributes
        /// </summary>
        AttributesCollections Attributes { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        Type PropertyType { get; }

        /// <summary>
        /// IsVirtual
        /// </summary>
        bool? IsVirtual { get; }

        /// <summary>
        /// Is a reference type eg not GetTypeInfo().IsClass
        /// </summary>
        bool IsInternalType { get; }

        /// <summary>
        /// Validate if attribute exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool ContainAttribute<T>() where T : Attribute;

        /// <summary>
        /// Validate if attribute type exist
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool ContainAttribute(Type type);

        /// <summary>
        /// Get first found attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetCustomAttribute<T>() where T : Attribute;

        /// <summary>
        /// Get first found attribute type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Attribute GetCustomAttribute(Type type);

        /// <summary>
        /// Exist only for PropertyInfo
        /// </summary>
        MethodInfo PropertyGetValue { get; }

        /// <summary>
        /// Exist only for PropertyInfo
        /// </summary>
        MethodInfo PropertySetValue { get; }
    }
}
