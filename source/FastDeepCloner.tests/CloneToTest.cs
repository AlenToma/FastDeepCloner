using FastDeepCloner.tests.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastDeepCloner.tests
{
    public class CloneToTest
    {
        [FastDeepCloner.FastDeepClonerColumn("Name")]
        public string FirstName { get; set; }

        public string PasswordLength { get; set; }

        [FastDeepClonerColumn("Test.myBar.Id")]
        public int Id { get; set; }

        public Circular Test { get; set; }
    }
}
