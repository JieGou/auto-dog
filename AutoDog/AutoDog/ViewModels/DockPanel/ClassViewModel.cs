using AutoDog.Logics;

namespace AutoDog.ViewModels
{
    class ClassViewModel : ToolViewModel
    {
        public const string ToolContentId = "ClassTool";
        public ClassViewModel()
            : base("类视图")
        {
            ContentId = ToolContentId;
            IconSource = Common.ConvertImageToBitMap("pack://application:,,/Images/DockPanel/ClassView.png");
        }
    }
}
