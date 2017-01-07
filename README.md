# FastDeepCloner-New-
FastDeepCloner is very fast portable library, This is a C# based .NET 4+ library that is used to deep clone objects, whether they are serializable or not. It intends to be much faster than the normal binary serialization method of deep cloning objects.

Update

Add Attribute [FastDeepClonerIgnore] to ignore cloning a property

How to use 

var mycar = FastDeepCloner.DeepCloner.Clone(mycar)

nuget:https://www.nuget.org/packages/FastDeepCloner/1.0.8
