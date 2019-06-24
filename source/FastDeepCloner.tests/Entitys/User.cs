using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FastDeepCloner.tests.Entitys
{
    public class User : Entity
    {

        public virtual string Name { get; set; } = "sdjh";

        public virtual int PasswordLength { get; set; } = 6;


        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

    }
}
