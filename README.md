# FastDeepCloner
FastDeepCloner is very fast portable library, This is a C# based .NET library that is used to deep clone objects, whether they are serializable or not. It intends to be much faster than the normal binary serialization method of deep cloning objects.

Add Attribute [FastDeepClonerIgnore] to ignore cloning a property

use FastDeepClonerSettings to override CreateInstance

## How to use

The library using Activator.CreateInstance for creating an object, you could override this setting and use FormatterServices.GetUninitializedObject and handle the creation of the object by assigning FastDeepClonerSettings.
In this case im ignoring all constructors by using GetUninitializedObject, unfortunately its not included in .net Core. 
```csharp
var settings = new FastDeepCloner.FastDeepClonerSettings() {
FieldType = FastDeepCloner.FieldType.FieldInfo,
OnCreateInstance = new FastDeepCloner.Extensions.CreateInstance((Type type) =>
{
return FormatterServices.GetUninitializedObject(type);
})
        
var mycar = FastDeepCloner.DeepCloner.Clone(mycar,settings);

```

Or Use the default and you will get an error if you dont have a default constructor

```csharp
var mycar = FastDeepCloner.DeepCloner.Clone(mycar);
```

nuget:https://www.nuget.org/packages/FastDeepCloner/
