using System;
using System.Collections.Generic;

namespace FastDeepCloner
{
    public interface IFastDeepClonerProperty
    {
        string Name { get; }

        string FullName { get; }

        void SetValue(object o, object value);

        object GetValue(object o);

        // able to process or clone
        bool CanRead { get; }

        // Have attr FastDeepClonerIgnore
        bool FastDeepClonerIgnore { get; }

        // All available attributes
        List<Attribute> Attributes { get; set; }

        Type PropertyType { get; }

        bool? IsVirtual { get; }

        /// <summary>
        /// Is a reference type eg IsClass
        /// </summary>
        bool IsInternalType { get; }

        bool ContainAttribute<T>() where T : Attribute, new();

        bool ContainAttribute(Type type);

    }
}
