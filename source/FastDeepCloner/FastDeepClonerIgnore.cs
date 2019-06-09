using System;
namespace FastDeepCloner
{
    /// <summary>
    /// Ignore Properties or Field that containe this attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class FastDeepClonerIgnore : Attribute
    {
    }
}
