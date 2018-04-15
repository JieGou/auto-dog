using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using AutoDog.Editor;
using AutoDog.Editor.CodeCompletion;
using AutoDog.Editor.Folding;
using AutoDog.Editor.Highlighting;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock.Layout;
using System.ComponentModel;
using AutoDog.UI.Controls.Dialogs;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using AutoDog.UI.Controls;
using AutoDog.TEControls.FolderBrowserControl;
using AutoDog.TEViews.AutoTesting.ViewModels;

namespace AutoDog.TEViews.AutoTesting
{
    /// <summary>
    /// MainAutoTesting.xaml 的交互逻辑
    /// </summary>
    public partial class MainAutoTesting :MetroWindow
    {

        #region 变量
        public static string solutionPath = null;
        private bool closeMe;
        public static MetroWindow AutoTestingWindow;
        #endregion
        public MainAutoTesting()
        {
            InitializeComponent();
            AutoTestingWindow = this;
            this.DataContext = Workspace.This;
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);

        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.LayoutSerializationCallback += (s, args) =>
            {
                args.Content = args.Content;
            };

            if (File.Exists(@".\AutoDog.Editor.config"))
                serializer.Deserialize(@".\AutoDog.Editor.config");
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(@".\AutoDog.Editor.config");
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
            return File.Exists(@".\AvalonDock.Layout.config");
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
            layoutSerializer.Deserialize(@".\AvalonDock.Layout.config");
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
            layoutSerializer.Serialize(@".\AvalonDock.Layout.config");
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
                solutionPath = folder.FileName;
                Properties.Settings.Default.solutionPath = solutionPath;
                Properties.Settings.Default.Save();
            }
        }


        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            var newProject = new Views.NewProject();
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
    }
}

