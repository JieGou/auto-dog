using System;
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
