# INotifyPropertyChanged Instance

You could create a type that implement INotifyPropertyChanged without adding a single extra code.

## Platform support
* .NETCoreApp 2.0
* .NETFramework 4.5.1
* .NETStandard 2.0

```csharp
public class User {

 public virtual string Name { get; set; }
 /// none virtual properties wont be included
 public string Name { get; set; }
 
  /// this is optional, FastDeepCloner will detect this method if it exist.
  private void PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    // your code here
  }
}


```
Now simple create the ProxyInstance

```csharp

  User pUser = DeepCloner.CreateProxyInstance<User>();     
               /// if you choose to not include the PropertyChanged in the class, you could simple bind it here 
               (pUser as INotifyPropertyChanged).PropertyChanged += ((sender, e)=> {  
                // your code here
               });
               
               pUser.Name = "testo";

```
