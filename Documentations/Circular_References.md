## Circular references

Here is how you could handle Circular references properties, by using `FastDeepClonerPrimaryIdentifireAttribute`

Example
 A class that have Circular references
 ```csharp
    public class Circular
    {
        // By adding this attribute the library will keep tracks of the objects that has already been cloned
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
        // By adding this attribute the library will keep tracks of the objects that has already been cloned
        [FastDeepClonerPrimaryIdentifire]
        public int Id { get; set; } = 105;
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
```

Now simple Clone the item
```csharp
     var item = new Circular();
     var cloned = FastDeepCloner.DeepCloner.Clone(item);
```
