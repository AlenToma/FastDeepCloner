## Here is two ways on how you could clone 
The library using IL for creating an object, you could override this setting and use your own handle for creation of object by assigning FastDeepClonerSettings.
By Default FastDeepCloner validate the type and check if it have a default constructor it will use IL for its fastest and if not it will use GetUninitializedObject and ignore all constructores
```csharp
var settings = new FastDeepCloner.FastDeepClonerSettings() {
FieldType = FastDeepCloner.FieldType.FieldInfo,
OnCreateInstance = new FastDeepCloner.Extensions.CreateInstance((Type type) =>
{
return FormatterServices.GetUninitializedObject(type);
})
        
var mycar = FastDeepCloner.DeepCloner.Clone(car,settings);


```
### Or Use the default

```csharp
var mycar = FastDeepCloner.DeepCloner.Clone(car);
```
