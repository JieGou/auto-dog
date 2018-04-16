using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AutoDog.ViewModels
{
    class OutPutViewModel:ToolViewModel
    {
        public const string ToolContentId = "OutPutTool";
        public OutPutViewModel()
            : base("输出")
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
