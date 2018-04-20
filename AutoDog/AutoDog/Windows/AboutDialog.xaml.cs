using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace AutoDog.Windows
{
  /// <summary>
  /// Interaction logic for AboutDialog.xaml
  /// </summary>
  public partial class AboutDialog : Window
  {
    public AboutDialog()
    {
      InitializeComponent();
    }


    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }


    private void OnNavigationRequest(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
    {
      Process.Start(e.Uri.ToString());
      e.Handled = true;
    }
  }
}