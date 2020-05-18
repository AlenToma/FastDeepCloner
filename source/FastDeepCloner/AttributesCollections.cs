using System;
using System.Collections.Generic;

namespace FastDeepCloner
{
    public class AttributesCollections : List<Attribute>
    {
        internal SafeValueType<Attribute, Attribute> ContainedAttributes = new SafeValueType<Attribute, Attribute>();
        internal SafeValueType<Type, Attribute> ContainedAttributestypes = new SafeValueType<Type, Attribute>();

        public AttributesCollections(List<Attribute> attrs)
        {
            if (attrs == null)
                return;
            foreach (Attribute attr in attrs)
                Add(attr);
        }

        public new void Add(Attribute attr)
        {
            ContainedAttributes.TryAdd(attr, attr, true);
            ContainedAttributestypes.TryAdd(attr.GetType(), attr, true);
            base.Add(attr);

        }

        public new void Remove(Attribute attr)
        {
            base.Remove(attr);
            ContainedAttributes.Remove(attr);
            ContainedAttributestypes.Remove(attr.GetType());
        }

    }
}
