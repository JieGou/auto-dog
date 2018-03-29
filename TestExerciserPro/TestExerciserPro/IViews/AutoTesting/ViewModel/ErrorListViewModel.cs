using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestExerciserPro.IViews.AutoTesting.ViewModel
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
            bi.UriSource = new Uri("pack://application:,,/IViews/AutoTesting/Images/property-blue.png");
            bi.EndInit();
            IconSource = bi;
        }
    }
}
