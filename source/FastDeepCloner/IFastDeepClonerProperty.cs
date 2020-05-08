using System;
using System.Collections.Generic;
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
        /// Incase you have circular references in some object
        /// You could mark an identifier or a primary key property so that fastDeepcloner could identify them
        /// </summary>
        bool FastDeepClonerPrimaryIdentifire { get; }

        /// <summary>
        /// Apply this to properties that cant be cloned, eg ImageSource and other controls.
        /// Those property will still be copied insted of cloning
        /// </summary>
        bool NoneCloneable { get; }

        /// <summary>
        /// All available attributes
        /// </summary>
        AttributesCollections Attributes { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        Type PropertyType { get; set; }

        /// <summary>
        /// IsVirtual
        /// </summary>
        bool? IsVirtual { get; }

        /// <summary>
        /// Is a reference type eg not GetTypeInfo().IsClass
        /// </summary>
        bool IsInternalType { get; }

        /// <summary>
        /// Get a collection of attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;


        /// <summary>
        /// Get a collection of attributes
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<Attribute> GetCustomAttributes(Type type);

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

        /// <summary>
        /// Using this method will trigger ConfigrationManager.OnAttributeCollectionChanged
        /// </summary>
        /// <param name="attr"></param>
        void Add(Attribute attr);
    }
}
