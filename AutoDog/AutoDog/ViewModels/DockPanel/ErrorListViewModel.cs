using System;
using AutoDog.Logics;

namespace AutoDog.ViewModels
{
    class ErrorListViewModel:ToolViewModel
    {
        public const string ToolContentId = "ErrorListTool";
        public ErrorListViewModel()
            : base("错误列表")
        {
            ContentId = ToolContentId;
            IconSource = Common.ConvertImageToBitMap("pack://application:,,/Images/DockPanel/ErrorList.png");
        }
    }
}
