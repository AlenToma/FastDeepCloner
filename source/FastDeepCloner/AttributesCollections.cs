using System;
using System.Collections.Generic;

namespace FastDeepCloner
{
    public class AttributesCollections : List<Attribute>
    {
        internal Dictionary<Attribute, Attribute> ContainedAttributes = new Dictionary<Attribute, Attribute>();
        internal Dictionary<Type, Attribute> ContainedAttributestypes = new Dictionary<Type, Attribute>();

        public AttributesCollections(List<Attribute> attrs)
        {
            if (attrs == null)
                return;
            foreach(Attribute attr in attrs)
            {
                ContainedAttributes.Add(attr, attr);
                ContainedAttributestypes.Add(attr.GetType(), attr);
                base.Add(attr);
            }
          
        }

        public new void Add(Attribute attr)
        {
            ContainedAttributes.Add(attr, attr);
            ContainedAttributestypes.Add(attr.GetType(), attr);
            base.Add(attr);
        }

        public new void Remove(Attribute attr)
        {
            this.Remove(attr);
            ContainedAttributes.Remove(attr);
            ContainedAttributestypes.Remove(attr.GetType());
        }

    }
}
