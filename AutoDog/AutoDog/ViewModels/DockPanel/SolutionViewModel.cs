using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using AutoDog.Logics;


namespace AutoDog.ViewModels
{
    class SolutionViewModel:ToolViewModel
    {
        public const string ToolContentId = "SolutionTool";

        public SolutionViewModel()
            :base("解决方案资源管理器")
        {
            ContentId = ToolContentId;
            IconSource = Common.ConvertImageToBitMap("pack://application:,,/Images/DockPanel/SolutionView.png");
        } 
    }
}
