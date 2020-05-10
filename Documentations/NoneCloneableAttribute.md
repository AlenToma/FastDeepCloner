## NoneCloneableAttribute 
 `NoneCloneableAttribute` to fix some issue with cloning GUI controls
 
 Sometime we have some GUI properties, like ImageSource, label, Buttons etc. Those properties cant be cloned but we still want to copy them
 `NoneCloneableAttribute` fix this issue.
 ```csharp
     public class TestControls
    {
        [NoneCloneable]
        public ImageSource Image { get; set; }
        
        public String Name {get; set;}

        public TestControls()
        {
            // Create a BitmapSource  
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRiwabCDDHq1NT1VHBsG4fLi8a5FhcEiiloEdCk23lTDYkst8Mz&usqp=CAU");
            bitmap.EndInit();
            Image = bitmap;
        }
    }
}
```
Now `Name` should be cloned and `Image` will be copied insted.

 ```csharp
            var test = new TestControls();
            var cloned = test.Clone();
```
