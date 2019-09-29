## CloneTo
You could clone an object to another object and also mapp between tow objects.
Here is how

we have two diffrent classes here

```csharp
    public class User : Entity
    {

        public virtual string Name { get; set; } = "sdjh";

        public virtual int PasswordLength { get; set; } = 6;

        public Circular Test { get; set; } = new Circular();

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

    }
    
    public class CloneToTest
    {
        [FastDeepCloner.FastDeepClonerColumn("Name")]
        public string FullName { get; set; }

        public string PasswordLength { get; set; }

        public Circular Test { get; set; }
    }

```

Now simple clone object 1 to object 2

```csharp
            var user = new User() { Name = "alen toma" };
            var cloneTo =new  CloneToTest();

            FastDeepCloner.DeepCloner.CloneTo(user, cloneTo);
      
            Assert.AreEqual(user.Name, cloneTo.FullName);

```
