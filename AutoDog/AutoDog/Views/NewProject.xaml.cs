using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using AutoDog.UI.Controls.Dialogs;
using AutoDog.Controls.FolderBrowserControl;
using AutoDog.ViewModels;

namespace AutoDog.Views
{
    /// <summary>
    /// NewSolution.xaml 的交互逻辑
    /// </summary>
    public partial class NewProject
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void openFolderClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.parentWindow = this;
            if (folder.ShowDialog() == true)
            {
                locationCmb.Text = folder.FileName;
            }
        }
    }
}
