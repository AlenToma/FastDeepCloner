using System;
using System.Collections.Generic;
using System.Text;

namespace FastDeepCloner
{

    /// <summary>
    /// Incase you have circular references in some object
    /// You could mark an identifier or a primary key property so that fastDeepcloner could identify them
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class FastDeepClonerPrimaryIdentifire : Attribute
    {
    }
}
