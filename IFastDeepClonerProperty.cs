using System;
namespace FastDeepCloner
{
    internal interface IFastDeepClonerProperty
    {

        string Name { get; set; }
        void SetValue(object o, object value);

        object GetValue(object o);

        bool CanRead { get; set; }

        bool IsInternalType { get; set; }
    }
}
