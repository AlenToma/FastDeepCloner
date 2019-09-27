using System;
using System.Collections.Generic;
using System.Text;

namespace BNGTest.Entitys
{
    public class CloneToTest
    {
        [FastDeepCloner.FastDeepClonerColumn("Name")]
        public string FirstName { get; set; }

        public string PasswordLength { get; set; }
    }
}
