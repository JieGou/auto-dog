using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TestExerciserPro.TEViews.AutoTesting.ViewModels;
using TestExerciserPro.TEViews.AutoTesting.Logic;

namespace TestExerciserPro.TEViews.AutoTesting.Views
{
    /// <summary>
    /// TreeSolution.xaml 的交互逻辑
    /// </summary>
    public partial class SolutionTemplateView : UserControl
    {
        public SolutionTemplateView()
        {
            InitializeComponent();
            SolutionTemplateViewInit();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SolutionTemplateViewInit();
        }

        public void SolutionTemplateViewInit()
        {
            var tviRoot = new TreeViewItem();

            GetSolutionTree(Properties.Settings.Default.solutionPath,tviRoot);
            TreeViewModel.SetItemImageName(tviRoot, @"../Images/Home.png");
            TreeViewModel.SetItemTypeName(tviRoot,ATConfig.TreeNodeType.RootNode.ToString());
            MySolutionTempView.Items.Add(tviRoot);
        }

        private void GetSolutionTree(string filePath,TreeViewItem tviRoot)
        {
            if (filePath != null && filePath != "")
            {
                //设置树根节点
                DirectoryInfo folder = new DirectoryInfo(filePath);
                tviRoot.Header = folder.Name;
                tviRoot.Tag = folder.FullName;

                //设置树文件节点
                FileInfo[] chldFiles = folder.GetFiles("*.*");
                foreach (FileInfo chlFile in chldFiles)
                {
                    TreeViewItem chldNode = new TreeViewItem();
                    chldNode.Header = chlFile.Name;
                    chldNode.Tag = chlFile.FullName;
                    TreeViewModel.SetItemImageName(chldNode, @"../Images/DocumentClosed.png");
                    TreeViewModel.SetItemTypeName(chldNode, ATConfig.TreeNodeType.FileNode.ToString());
                    //string ext = chlFile.Name.Substring(chlFile.Name.LastIndexOf(".") + 1, (chlFile.Name.Length - chlFile.Name.LastIndexOf(".") - 1));
                    tviRoot.Items.Add(chldNode);
                }

                //设置树文件夹节点
                DirectoryInfo[] chldFolders = folder.GetDirectories();
                foreach (DirectoryInfo chldFolder in chldFolders)
                {
                    TreeViewItem chldNode = new TreeViewItem();
                    chldNode.Header = folder.Name;
                    chldNode.Tag = folder.FullName;
                    chldNode.Expanded += ChldNode_Expanded;
                    chldNode.Collapsed += ChldNode_Collapsed;
                    TreeViewModel.SetItemImageName(chldNode, @"../Images/FolderClosed.png");
                    TreeViewModel.SetItemTypeName(chldNode, ATConfig.TreeNodeType.FolderNode.ToString());
                    tviRoot.Items.Add(chldNode);
                    GetSolutionTree(chldFolder.FullName, chldNode);
                }
            }
           }

        private void ChldNode_Collapsed(object sender, RoutedEventArgs e)
        {
            var tviSender = e.OriginalSource as TreeViewItem;
            SetItemStyles(tviSender, @"../Images/FolderClosed.png");
        }

        private void ChldNode_Expanded(object sender, RoutedEventArgs e)
        {
            var tviSender = e.OriginalSource as TreeViewItem;
            SetItemStyles(tviSender, @"../Images/FolderOpened.png");
        }

        private void MySolutionTempView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tviNew = (TreeViewItem)e.NewValue;
            SetItemStyles(tviNew, @"../Images/FolderSelected.png", @"../Images/DocumentSelected.png");
            if (e.OldValue != null)
            {
                var tviOld = (TreeViewItem)e.OldValue;
                if (TreeViewModel.GetItemTypeName(tviOld) == ATConfig.TreeNodeType.FolderNode.ToString() && tviOld.IsExpanded == true)
                {
                    SetItemStyles(tviOld, @"../Images/FolderOpened.png");
                }             
                else
                {
                    SetItemStyles(tviOld, @"../Images/FolderClosed.png", @"../Images/DocumentClosed.png");
                }       
            }                         
        }

        void SetItemStyles(TreeViewItem tvi, string folderImage, string fileImage)
        {
            if (TreeViewModel.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FolderNode.ToString())
            {
                TreeViewModel.SetItemImageName(tvi, folderImage);
            }
            else if (TreeViewModel.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FileNode.ToString())
            {
                TreeViewModel.SetItemImageName(tvi, fileImage);
            }
        }

        void SetItemStyles(TreeViewItem tvi, string folderImage)
        {
            if (TreeViewModel.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FolderNode.ToString())
            {
                TreeViewModel.SetItemImageName(tvi, folderImage);
            }
        }

        private void MySolutionTempView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void MySolutionTempView_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }
    }
}
