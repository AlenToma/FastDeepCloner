# Introduction FastDeepCloner

FastDeepCloner is very fast portable library, This is a C# based .NET library that is used to deep clone objects, whether they are serializable or not. It intends to be much faster than the normal binary serialization method of deep cloning objects.

## Nuget

https://www.nuget.org/packages/FastDeepCloner/

## Attributes

Add Attribute [FastDeepClonerIgnore] to ignore cloning a property



## How to use
#### Use FastDeepClonerSettings to override CreateInstance.

The library using Activator.CreateInstance for creating an object, you could override this setting and use FormatterServices.GetUninitializedObject and handle the creation of the object by assigning FastDeepClonerSettings.
In this case im ignoring all constructors by using GetUninitializedObject, unfortunately its not included in .net Core. 

### Code Example with Manually creating an instance
```
var settings = new FastDeepCloner.FastDeepClonerSettings() {
FieldType = FastDeepCloner.FieldType.FieldInfo,
OnCreateInstance = new FastDeepCloner.Extensions.CreateInstance((Type type) =>
{
return FormatterServices.GetUninitializedObject(type);
})
        
var mycar = FastDeepCloner.DeepCloner.Clone(mycar,settings);

```
### Using the default eg Activator.CreateInstance

You will get an error if you dont have a default constructor

```
var mycar = FastDeepCloner.DeepCloner.Clone(mycar);
```
