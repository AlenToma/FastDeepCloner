using System;
using System.Reflection;
using System.Linq;

namespace FastDeepCloner
{
    internal class FastDeepClonerProperty : IFastDeepClonerProperty
    {
        private Func<object, object> _propertyGet;

        private Action<object, object> _propertySet;

        public bool CanRead { get; private set; }

        public bool FastDeepClonerIgnore { get; private set; }

        public string Name { get; private set; }

        public string FullName { get; private set; }

        public bool IsInternalType { get; private set; }

        public Type PropertyType { get; private set; }

        public bool? IsVirtual { get; private set; }

        public AttributesCollections Attributes { get; set; }

        internal FastDeepClonerProperty(FieldInfo field)
        {

            CanRead = !(field.IsInitOnly || field.FieldType == typeof(IntPtr));
            FastDeepClonerIgnore = field.GetCustomAttribute<FastDeepClonerIgnore>() != null;
            Attributes = new AttributesCollections(field.GetCustomAttributes().ToList());
            _propertyGet = field.GetValue;
            _propertySet = field.SetValue;
            Name = field.Name;
            FullName = field.FieldType.FullName;
            PropertyType = field.FieldType;
            IsInternalType = field.FieldType.IsInternalType();
        }

        internal FastDeepClonerProperty(PropertyInfo property)
        {
            CanRead = !(!property.CanWrite || !property.CanRead || property.PropertyType == typeof(IntPtr));
            FastDeepClonerIgnore = property.GetCustomAttribute<FastDeepClonerIgnore>() != null;
            _propertyGet = property.GetValue;
            _propertySet = property.SetValue;
            Attributes = new AttributesCollections(property.GetCustomAttributes().ToList());
            Name = property.Name;
            FullName = property.PropertyType.FullName;
            PropertyType = property.PropertyType;
            IsInternalType = property.PropertyType.IsInternalType();
            IsVirtual = property.GetMethod.IsVirtual;
        }

        public bool ContainAttribute<T>() where T : Attribute, new()
        {
            return Attributes?.ContainedAttributestypes.ContainsKey(typeof(T)) ?? false;
        }

        public T GetCustomAttribute<T>() where T : Attribute, new()
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
            _propertySet(o, value);
        }

        public object GetValue(object o)
        {
            return _propertyGet(o);
        }
    }
}
