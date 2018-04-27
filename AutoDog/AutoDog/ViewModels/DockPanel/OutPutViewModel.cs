using System;
using AutoDog.Logics;

namespace AutoDog.ViewModels
{
    class OutPutViewModel:ToolViewModel
    {
        public const string ToolContentId = "OutPutTool";
        public OutPutViewModel()
            : base("输出")
        {
            ContentId = ToolContentId;
            IconSource = Common.ConvertImageToBitMap("pack://application:,,/Images/DockPanel/OutPut.png");
        }
    }
}
