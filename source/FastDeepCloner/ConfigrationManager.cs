using System;

namespace FastDeepCloner
{
    /// <summary>
    /// globa ConfigrationManager, for managing the library settings
    /// </summary>
    public static class ConfigrationManager
    {
        // this will trigger when a new PropertyInfo, FieldInfo Type is applied to IFastDeepClonerProperty
        // you could handle which type the IFastDeepClonerProperty PropertyType should containe.
        public static Func<IFastDeepClonerProperty, Type> OnPropertTypeSet;

        // This will trigger when a new attribute is added, you could make some changes to IFastDeepClonerProperty
        public static Action<IFastDeepClonerProperty> OnAttributeCollectionChanged;

    }
}
