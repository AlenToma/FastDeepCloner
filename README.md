# FastDeepCloner-New-
FastDeepCloner is very fast portable library, This is a C# based .NET 4+ library that is used to deep clone objects, whether they are serializable or not. It intends to be much faster than the normal binary serialization method of deep cloning objects.

Update
Add Attribute [FastDeepClonerIgnore] to ignore cloning a property
usie FastDeepClonerSettings to override CreateInstance

how to use

<code>
var settings = new FastDeepCloner.FastDeepClonerSettings() {
                FieldType = FastDeepCloner.FieldType.FieldInfo,
                OnCreateInstance = new FastDeepCloner.Extensions.CreateInstance((Type type) =>
                {
                    return FormatterServices.GetUninitializedObject(type);
								})
        
var mycar = FastDeepCloner.DeepCloner.Clone(mycar,settings);

//OR
var mycar = FastDeepCloner.DeepCloner.Clone(mycar);

</code> 

nuget:https://www.nuget.org/packages/FastDeepCloner/
