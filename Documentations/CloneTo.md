## CloneTo
You could clone an object to another object and also mapp between tow objects.
Here is how

we have two diffrent classes here

```csharp
    public class User 
    {

        public virtual string Name { get; set; } = "sdjh";

        public virtual int PasswordLength { get; set; } = 6;

        public Circular Test { get; set; } = new Circular();

    }
    
    public class CloneToTest
    {
        [FastDeepCloner.FastDeepClonerColumn("Name")] 
        [FastDeepCloner.FastDeepClonerColumn("LastName")] // Or
        public string FullName { get; set; }
        
        // You see here the type could be difrrent then the orginal type. 
        // FastDeepCloner will try to convert it, if it fail then a default value will be inserted insted
        public string PasswordLength { get; set; }
        
        // You could add a path insted, remember this only work on none list items.
        [FastDeepClonerColumn("Test.myBar.Id")]
        public int Id { get; set; }

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
