using System;
using System.Collections.Generic;
using System.Text;

namespace FastDeepCloner.tests.Entitys
{
    public class Circular
    {
        [FastDeepClonerPrimaryIdentifire]
        public int Id { get; set; } = 1;
        private Bar _bar;
        public Bar myBar
        {
            get
            {
                if (_bar == null)
                    _bar = new Bar();
                return _bar;
            }

            set => _bar = value;

        }

        public Circular()
        {
            myBar = new Bar();
        }
    }

    public class Bar
    {
        [FastDeepClonerPrimaryIdentifire]
        public int Id { get; set; } = 1;
        private Circular _myFoo;
        public Circular myFoo
        {
            get
            {
                if (_myFoo == null)
                    _myFoo = new Circular();
                return _myFoo;
            }

            set => _myFoo = value;

        }

    }
}
