using System.Windows.Controls;
using TestExerciserPro.UI.Controls;

namespace TestExerciserPro.TestViews
{
    /// <summary>
    /// Interaction logic for TabControlExamples.xaml
    /// </summary>
    public partial class TabControlExamples : UserControl
    {
        public TabControlExamples()
        {
            InitializeComponent();
        }

        private void MetroTabControl_TabItemClosingEvent(object sender, BaseMetroTabControl.TabItemClosingEventArgs e)
        {
            if (e.ClosingTabItem.Header.ToString().StartsWith("sizes"))
                e.Cancel = true;
        }
    }
}
