using System;
using System.Reflection;
using System.Linq;

namespace FastDeepCloner
{
    public class FastDeepClonerProperty : IFastDeepClonerProperty
    {
        public Func<object, object> GetMethod { get; set; }

        public Action<object, object> SetMethod { get; set; }

        public bool CanRead { get; private set; }

        public bool FastDeepClonerIgnore { get; private set; }

        public string Name { get; private set; }

        public string FullName { get; private set; }

        public bool IsInternalType { get; private set; }

        public Type PropertyType { get; private set; }

        public bool? IsVirtual { get; private set; }

        public AttributesCollections Attributes { get; set; }

        public MethodInfo PropertyGetValue { get; private set; }

        public MethodInfo PropertySetValue { get; private set; }

        internal FastDeepClonerProperty(FieldInfo field)
        {

            CanRead = !(field.IsInitOnly || field.FieldType == typeof(IntPtr) || field.IsLiteral);
            FastDeepClonerIgnore = field.GetCustomAttribute<FastDeepClonerIgnore>() != null;
            Attributes = new AttributesCollections(field.GetCustomAttributes().ToList());
            GetMethod = field.GetValue;
            SetMethod = field.SetValue;
            Name = field.Name;
            FullName = field.FieldType.FullName;
            PropertyType = field.FieldType;
            IsInternalType = field.FieldType.IsInternalType();
        }

        internal FastDeepClonerProperty(PropertyInfo property)
        {
            CanRead = !(!property.CanWrite || !property.CanRead || property.PropertyType == typeof(IntPtr) || property.GetIndexParameters().Length > 0);
            FastDeepClonerIgnore = property.GetCustomAttribute<FastDeepClonerIgnore>() != null;
            GetMethod = property.GetValue;
            SetMethod = property.SetValue;
            Attributes = new AttributesCollections(property.GetCustomAttributes().ToList());
            Name = property.Name;
            FullName = property.PropertyType.FullName;
            PropertyType = property.PropertyType;
            IsInternalType = property.PropertyType.IsInternalType();
            IsVirtual = property.GetMethod.IsVirtual;
            PropertyGetValue = property.GetMethod;
            PropertySetValue = property.SetMethod;

        }

        public bool ContainAttribute<T>() where T : Attribute
        {
            return Attributes?.ContainedAttributestypes.ContainsKey(typeof(T)) ?? false;
        }

        public T GetCustomAttribute<T>() where T : Attribute
        {
            return ContainAttribute<T>() ? (T)Attributes?.ContainedAttributestypes[typeof(T)] : null;
        }

        public Attribute GetCustomAttribute(Type type)
        {
            return ContainAttribute(type) ? Attributes?.ContainedAttributestypes[type] : null;
        }

        public bool ContainAttribute(Type type)
        {
            return Attributes?.ContainedAttributestypes.ContainsKey(type) ?? false;
        }

        public void SetValue(object o, object value)
        {
            SetMethod(o, value);
        }

        public object GetValue(object o)
        {
            return GetMethod(o);
        }
    }
}
