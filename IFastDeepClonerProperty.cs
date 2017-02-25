using System;
namespace FastDeepCloner
{
    public interface IFastDeepClonerProperty
    {
        string Name { get; }

        void SetValue(object o, object value);

        object GetValue(object o);

        bool CanRead { get; }

        Type PropertyType { get; }

        bool? IsVirtual { get; }

        /// <summary>
        /// Is a reference type eg IsClass
        /// </summary>
        bool IsInternalType { get; }
    }
}
