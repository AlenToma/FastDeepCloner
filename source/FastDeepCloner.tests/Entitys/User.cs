using System;
using System.Collections.Generic;
using System.Text;

namespace FastDeepCloner.tests.Entitys
{
    public class User : Entity
    {
        public string Name { get; set; } = "sdjh";

        public int PasswordLength { get; set; } = 6;
        
    }
}
