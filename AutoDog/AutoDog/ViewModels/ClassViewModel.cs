using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace AutoDog.ViewModels
{
    class ClassViewModel : ToolViewModel
    {
        public const string ToolContentId = "ClassTool";
        public ClassViewModel()
            : base("类视图")
        {
            ContentId = ToolContentId;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("pack://application:,,/Images/PropertyView.png");
            bi.EndInit();
            IconSource = bi;
        }
    }
}
