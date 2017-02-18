using System;
namespace FastDeepCloner
{
    public interface IFastDeepClonerProperty
    {
        string Name { get; set; }

        void SetValue(object o, object value);

        object GetValue(object o);

        bool CanRead { get; set; }

        /// <summary>
        /// Is a reference type eg IsClass
        /// </summary>
        bool IsInternalType { get; set; }
    }
}
