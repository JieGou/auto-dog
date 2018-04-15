using System.Windows;

namespace AutoDog.UI.Controls
{
    /// <summary>
    /// The HamburgerMenuItemCollection provides typed collection of HamburgerMenuItem.
    /// </summary>
    public class HamburgerMenuItemCollection : FreezableCollection<HamburgerMenuItem>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new HamburgerMenuItemCollection();
        }
    }
}
