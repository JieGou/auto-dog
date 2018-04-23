using System.IO;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Layout;
using System.ComponentModel;
using AutoDog.UI.Controls.Dialogs;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using AutoDog.UI.Controls;
using AutoDog.Controls.FolderBrowserControl;
using AutoDog.ViewModels;
using AutoDog.Windows.ProjectManager;

namespace AutoDog
{
    /// <summary>
    /// MainAutoTesting.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    {

        #region 变量
        public static MetroWindow metroWindow;
        private bool closeMe;       
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            metroWindow = this;
            this.DataContext = Workspace.This;
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);

        }

        public static readonly DependencyProperty ToggleFullScreenProperty =
    DependencyProperty.Register("ToggleFullScreen",
                                typeof(bool),
                                typeof(MainWindow),
                                new PropertyMetadata(default(bool), ToggleFullScreenPropertyChangedCallback));

        private static void ToggleFullScreenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var metroWindow = (MetroWindow)dependencyObject;
            if (e.OldValue != e.NewValue)
            {
                var fullScreen = (bool)e.NewValue;
                if (fullScreen)
                {
                    metroWindow.IgnoreTaskbarOnMaximize = true;
                    metroWindow.WindowState = WindowState.Maximized;
                    metroWindow.UseNoneWindowStyle = true;
                }
                else
                {
                    metroWindow.WindowState = WindowState.Normal;
                    metroWindow.UseNoneWindowStyle = false;
                    metroWindow.ShowTitleBar = true; // <-- this must be set to true
                    metroWindow.IgnoreTaskbarOnMaximize = false;
                }
            }
        }

        public bool ToggleFullScreen
        {
            get { return (bool)GetValue(ToggleFullScreenProperty); }
            set { SetValue(ToggleFullScreenProperty, value); }
        }

        public static readonly DependencyProperty UseAccentForDialogsProperty =
            DependencyProperty.Register("UseAccentForDialogs",
                                        typeof(bool),
                                        typeof(MainWindow),
                                        new PropertyMetadata(default(bool), ToggleUseAccentForDialogsPropertyChangedCallback));

        private static void ToggleUseAccentForDialogsPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var metroWindow = (MetroWindow)dependencyObject;
            if (e.OldValue != e.NewValue)
            {
                var useAccentForDialogs = (bool)e.NewValue;
                metroWindow.MetroDialogOptions.ColorScheme = useAccentForDialogs ? MetroDialogColorScheme.Accented : MetroDialogColorScheme.Theme;
            }
        }

        public bool UseAccentForDialogs
        {
            get { return (bool)GetValue(UseAccentForDialogsProperty); }
            set { SetValue(UseAccentForDialogsProperty, value); }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.LayoutSerializationCallback += (s, args) =>
            {
                args.Content = args.Content;
            };

            if (File.Exists(@".\AutoDog.config"))
                serializer.Deserialize(@".\AutoDog.config");
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(@".\AutoDog.config");
        }

        #region LoadLayoutCommand
        RelayCommand _loadLayoutCommand = null;
        public ICommand LoadLayoutCommand
        {
            get
            {
                if (_loadLayoutCommand == null)
                {
                    _loadLayoutCommand = new RelayCommand((p) => OnLoadLayout(p), (p) => CanLoadLayout(p));
                }

                return _loadLayoutCommand;
            }
        }

        private bool CanLoadLayout(object parameter)
        {
            return File.Exists(@".\AutoDog.config");
        }

        private void OnLoadLayout(object parameter)
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
            {
                if (e.Model.ContentId == FileStatsViewModel.ToolContentId)
                    e.Content = Workspace.This.FileStats;
                else if (e.Model.ContentId == SolutionViewModel.ToolContentId)
                    e.Content = Workspace.This.SolutionView;
                else if (e.Model.ContentId == ClassViewModel.ToolContentId)
                    e.Content = Workspace.This.ClassView;
                else if (!string.IsNullOrWhiteSpace(e.Model.ContentId) &&
                    File.Exists(e.Model.ContentId))
                    e.Content = Workspace.This.Open(e.Model.ContentId);
            };
            layoutSerializer.Deserialize(@".\AutoDog.config");
        }

        #endregion 

        #region SaveLayoutCommand
        RelayCommand _saveLayoutCommand = null;
        public ICommand SaveLayoutCommand
        {
            get
            {
                if (_saveLayoutCommand == null)
                {
                    _saveLayoutCommand = new RelayCommand((p) => OnSaveLayout(p), (p) => CanSaveLayout(p));
                }

                return _saveLayoutCommand;
            }
        }

        private bool CanSaveLayout(object parameter)
        {
            return true;
        }

        private void OnSaveLayout(object parameter)
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(@".\AutoDog.config");
        }

        #endregion 

        private void OnDumpToConsole(object sender, RoutedEventArgs e)
        {
            // Uncomment when TRACE is activated on AvalonDock project
            dockManager.Layout.ConsoleDump(0);
        }

 
        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Title = "打开项目";
            if (folder.ShowDialog() == true)
            {
                Properties.Settings.Default.solutionPath = folder.FileName;
                Properties.Settings.Default.Save();
            }
        }


        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            var newProject = new NewSolution();
            newProject.Show();
        }

        private async void layOutAnc_DocumentClosing(object sender, CancelEventArgs e)
        {
            var currentLayOutAnc = sender as LayoutAnchorable;
            if (e.Cancel) return;
            e.Cancel = !this.closeMe;
            if (this.closeMe) return;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "退出",
                NegativeButtonText = "取消",
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(
                "关闭当前文档？",
                "确定要关闭当前文档吗？关闭后，未保存内容将会丢失！",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

                this.closeMe = result == MessageDialogResult.Affirmative;

            if (this.closeMe) currentLayOutAnc.Close();
        }

        private async void MainAutoTesting_WinddowClosing(object sender, CancelEventArgs e)
        {
            if (e.Cancel) return;
            e.Cancel = !this.closeMe;
            if (this.closeMe) return;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "退出",
                NegativeButtonText = "取消",
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(
                "退出窗口?",
                "确定要退出当前窗口吗?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            this.closeMe = result == MessageDialogResult.Affirmative;

            if (this.closeMe) this.Close();
        }

        private void MainWindowExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LaunchAppsOnGitHub(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/devdiv/AutoDog");
        }

        private async void CloseCustomDialog(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            await this.HideMetroDialogAsync(dialog);
            await this.ShowMessageAsync("Dialog gone", "The custom dialog has closed");
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            var newFile = new NewFile();
            newFile.Show();
        }

        private void AboutAutoDog_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new Windows.AboutDialog();
            aboutWindow.Show();
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

