# [FastDeepCloner](https://www.nuget.org/packages/FastDeepCloner/)
[FastDeepCloner](https://www.nuget.org/packages/FastDeepCloner/) is very fast portable library, This is a C# based .NET library that is used to deep clone objects, whether they are serializable or not. It intends to be much faster than the normal binary serialization method of deep cloning objects.

Add Attribute [FastDeepClonerIgnore] to ignore cloning a property

use FastDeepClonerSettings to override CreateInstance

## How to use

The library using IL for creating an object, you could override this setting and use your own and handle the creation of the object by assigning FastDeepClonerSettings.
In this case im ignoring all constructors by using GetUninitializedObject.
FastDeepCloner validate the type and check if it have a default constructor it will use IL for it fastest and if not it will use GetUninitializedObject and ignore all constructores

## Code Example
* [Clone](https://github.com/AlenToma/FastDeepCloner/blob/master/Documentations/Clone.md)
* [InternalType ](https://github.com/AlenToma/FastDeepCloner/blob/master/Documentations/InternalTypes.md)
