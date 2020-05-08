using FastDeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FormTest
{
    public class TestControls
    {
        [NoneCloneable]
        public ImageSource Image { get; set; }

        [NoneCloneable]
        public Button Btn { get; set; }

        public TestControls()
        {
            // Create a BitmapSource  
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRiwabCDDHq1NT1VHBsG4fLi8a5FhcEiiloEdCk23lTDYkst8Mz&usqp=CAU");
            bitmap.EndInit();
            Image = bitmap;

            Btn = new Button()
            {
                Content = "test"
            };
        }
    }
}
