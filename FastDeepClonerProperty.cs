using System;
using System.Reflection;

namespace FastDeepCloner
{
    internal class FastDeepClonerProperty : IFastDeepClonerProperty
    {
        private Func<object, object> _propertyGet;

        private Action<object, object> _propertySet;

        public bool CanRead { get; set; }

        public string Name { get; set; }

        public bool IsInternalType { get; set; }

        internal FastDeepClonerProperty(FieldInfo field)
        {
            CanRead = !(field.IsInitOnly || field.FieldType == typeof(IntPtr) || field.GetCustomAttribute<FastDeepClonerIgnore>() != null);
            _propertyGet = field.GetValue;
            _propertySet = field.SetValue;
            Name = field.Name;
            IsInternalType = field.FieldType.IsInternalType();
        }

        internal FastDeepClonerProperty(PropertyInfo property)
        {
            CanRead = !((!property.CanWrite || !property.CanRead || property.PropertyType == typeof(IntPtr) || property.GetCustomAttribute<FastDeepClonerIgnore>() != null));
            _propertyGet = (Func<object, object>)property.GetValue;
            _propertySet = (Action<object, object>)property.SetValue;
            Name = property.Name;
            IsInternalType = property.PropertyType.IsInternalType();
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
