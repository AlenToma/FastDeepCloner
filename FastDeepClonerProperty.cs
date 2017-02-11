using System;
using System.Reflection;

namespace FastDeepCloner
{
    internal class FastDeepClonerProperty : IFastDeepClonerProperty
    {
        private FieldInfo _field;

        private PropertyInfo _property;

        public bool CanRead { get; set; }

        public string Name { get; set; }

        public bool IsInternalType { get; set; }

        internal FastDeepClonerProperty(FieldInfo field)
        {
            CanRead = !(field.IsInitOnly || field.FieldType == typeof(IntPtr) || field.GetCustomAttribute<FastDeepClonerIgnore>() != null);
            _field = field;
            IsInternalType = _field.FieldType.IsInternalType();
            Name = field.Name;
        }

        internal FastDeepClonerProperty(PropertyInfo property)
        {
            CanRead = !((!property.CanWrite || !property.CanRead || property.PropertyType == typeof(IntPtr) || property.GetCustomAttribute<FastDeepClonerIgnore>() != null));
            _property = property;
            Name = property.Name;
            IsInternalType = property.PropertyType.IsInternalType();
        } 

        public void SetValue(object o, object value)
        {
            if (_field != null)
                _field.SetValue(o, value);
            else _property.SetValue(o, value);
        }

        public object GetValue(object o)
        {
            if (_field != null)
                return _field.GetValue(o);
            else return _property.GetValue(o);
        }


    }
}
