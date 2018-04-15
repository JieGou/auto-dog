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
using AutoDog.TEControls.FolderBrowserControl;
using AutoDog.TEViews.AutoTesting.ViewModels;

namespace AutoDog.TEViews.AutoTesting.Views
{
    /// <summary>
    /// NewSolution.xaml 的交互逻辑
    /// </summary>
    public partial class NewProject
    {

        private readonly NewProjectViewModel _viewModel;
        public NewProject()
        {
            _viewModel = new NewProjectViewModel(DialogCoordinator.Instance);
            DataContext = _viewModel;

            InitializeComponent();
            this.DataContextChanged += (sender, args) => {
                var vm = args.NewValue as NewProjectViewModel;
                if (vm != null)
                {
                    CollectionViewSource.GetDefaultView(vm.Albums).GroupDescriptions.Clear();
                    CollectionViewSource.GetDefaultView(vm.Albums).GroupDescriptions.Add(new PropertyGroupDescription("Artist"));
                }
            };
        }

        private void openFolderClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == true)
            {
                locationCmb.Text = folder.FileName;
            }
        }
    }
}
