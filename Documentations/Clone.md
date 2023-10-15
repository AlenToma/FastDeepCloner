## Here are two ways with which you can clone 
Depending on the platform, the library using `IL` for creating an object, you could override this setting and use your own handler for creation of an object by assigning `FastDeepClonerSettings`.
By Default `FastDeepCloner` validates the type and checks if it has a default constructor. It will use `IL` for the fastest way and, if not, it will use `GetUninitializedObject` and ignore all constructors.

```csharp
var settings = new FastDeepCloner.FastDeepClonerSettings() 
{
        FieldType = FastDeepCloner.FieldType.FieldInfo,
        OnCreateInstance = new FastDeepCloner.Extensions.CreateInstance((Type type) =>
        {
                return FormatterServices.GetUninitializedObject(type);
        })
};
        
var mycar = FastDeepCloner.DeepCloner.Clone(car,settings);


```
### Or Use the default

```csharp
var mycar = FastDeepCloner.DeepCloner.Clone(car);
```
