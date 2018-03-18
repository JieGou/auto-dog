using System.Windows.Controls;

namespace TestExerciserPro
{
    using TestExerciserPro.UI.Controls;

    public sealed partial class HamburgerMenuRipple : UserControl
    {
        public HamburgerMenuRipple()
        {
            this.InitializeComponent();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            HamburgerMenuControl.Content = e.InvokedItem;
        }
    }
}