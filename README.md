# FastDeepCloner-New-
FastDeepCloner is very fast portable library, This is a C# based .NET 4+ library that is used to deep clone objects, whether they are serializable or not. It intends to be much faster than the normal binary serialization method of deep cloning objects.

Add Attribute [FastDeepClonerIgnore] to ignore cloning a property

use FastDeepClonerSettings to override CreateInstance

how to use

<code>
The library using Activator.CreateInstance for creating an object, you could override this setting and use FormatterServices.GetUninitializedObject and handle the creation of the object by assigning FastDeepClonerSettings.
In this case im ignoring all constructors by using GetUninitializedObject, unfortunately its not included in .net Core. 

var settings = new FastDeepCloner.FastDeepClonerSettings() {
FieldType = FastDeepCloner.FieldType.FieldInfo,
OnCreateInstance = new FastDeepCloner.Extensions.CreateInstance((Type type) =>
{
return FormatterServices.GetUninitializedObject(type);
})
        
var mycar = FastDeepCloner.DeepCloner.Clone(mycar,settings);

OR

Use the default and you will get an error if you dont have a default constructor

var mycar = FastDeepCloner.DeepCloner.Clone(mycar);

</code> 

nuget:https://www.nuget.org/packages/FastDeepCloner/
