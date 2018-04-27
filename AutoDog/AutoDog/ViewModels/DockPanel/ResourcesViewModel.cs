using System;
using AutoDog.Logics;

namespace AutoDog.ViewModels
{
    class ResourcesViewModel: ToolViewModel
    {
        public const string ToolContentId = "ResourcesTool";

        public ResourcesViewModel()
            :base("资源视图")
        {
            ContentId = ToolContentId;
            IconSource = Common.ConvertImageToBitMap("pack://application:,,/Images/DockPanel/ResourceView.png");
        }
    }
}
