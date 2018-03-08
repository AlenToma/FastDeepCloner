## Here is two ways on how you could clone 

```csharp
var settings = new FastDeepCloner.FastDeepClonerSettings() {
FieldType = FastDeepCloner.FieldType.FieldInfo,
OnCreateInstance = new FastDeepCloner.Extensions.CreateInstance((Type type) =>
{
return FormatterServices.GetUninitializedObject(type);
})
        
var mycar = FastDeepCloner.DeepCloner.Clone(mycar,settings);


```
### Or Use the default and you will get an error if you dont have a default constructor

```csharp
var mycar = FastDeepCloner.DeepCloner.Clone(mycar);
```
