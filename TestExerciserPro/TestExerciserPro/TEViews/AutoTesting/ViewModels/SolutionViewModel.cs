using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;


namespace TestExerciserPro.TEViews.AutoTesting.ViewModels
{
    class SolutionViewModel:ToolViewModel
    {
        public const string ToolContentId = "SolutionTool";

        public SolutionViewModel()
            :base("解决方案资源管理器")
        {
            ContentId = ToolContentId;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("pack://application:,,/TEViews/AutoTesting/Images/property-blue.png");
            bi.EndInit();
            IconSource = bi;
        } 
    }
}
