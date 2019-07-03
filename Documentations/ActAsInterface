## Object to interface converter
You could convert any object to an interface, even AnonymousTypes with easy. 
the object offcouse wont need to be inherited from the interface, the library will do this instead.

Example
```csharp
    public interface IUser
    {
        string Name { get; set; }
    }
    
    public class User
    {
        public virtual string Name { get; set; } = "sdjh";

        public virtual int PasswordLength { get; set; } = 6;
    }
```

Now you could simple convert User to IUser with easy.

```csharp
  User user = new User() { Name="Test" };
  IUser iUser = user.ActAsInterface<IUser>();
  
  // You could even use a AnonymousType
  IUser iUser = new { Name= "Test" }.ActAsInterface<IUser>();

```

