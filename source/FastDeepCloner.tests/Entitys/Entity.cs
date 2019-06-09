using System;
using System.Collections.Generic;
using System.Text;

namespace FastDeepCloner.tests.Entitys
{
    public abstract class Entity
    {
        public virtual long Id { get; set; } = 0;

        public virtual Guid Id2 { get; set; } = Guid.NewGuid();
    }
}
