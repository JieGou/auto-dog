using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AutoDog.TEViews.AutoTesting.ViewModels
{
    class ErrorListViewModel:ToolViewModel
    {
        public const string ToolContentId = "ErrorListTool";
        public ErrorListViewModel()
            : base("错误列表")
        {
            ContentId = ToolContentId;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("pack://application:,,/TEViews/AutoTesting/Images/PropertyView.png");
            bi.EndInit();
            IconSource = bi;
        }
    }
}
