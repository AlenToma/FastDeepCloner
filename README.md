# FastDeepCloner-New-
FastDeepCloner is very fast portable library, This is a C# based .NET 4+ library that is used to deep clone objects, whether they are serializable or not. It intends to be much faster than the normal binary serialization method of deep cloning objects.

Update
Add Attribute [FastDeepClonerIgnore] to ignore cloning a property

How to use 
Car myobject = FastDeepCloner.DeepCloner.Clone<Car>(myCar);
List<Car> myobjectList = FastDeepCloner.DeepCloner.Clone<List<Car>>(myCars);
var mycar = FastDeepCloner.DeepCloner.Clone(mycar)
