using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AutoDog.ViewModels
{
    class ResourcesViewModel: ToolViewModel
    {
        public const string ToolContentId = "ResourcesTool";

        public ResourcesViewModel()
            :base("资源视图")
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
