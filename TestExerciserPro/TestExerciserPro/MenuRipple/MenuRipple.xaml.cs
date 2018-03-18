using System.Windows.Controls;

namespace TestExerciserPro
{
    using TestExerciserPro.UI.Controls;

    public sealed partial class MenuRipple : UserControl
    {
        public MenuRipple()
        {
            this.InitializeComponent();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            HamburgerMenuControl.Content = e.InvokedItem;
        }
    }
}