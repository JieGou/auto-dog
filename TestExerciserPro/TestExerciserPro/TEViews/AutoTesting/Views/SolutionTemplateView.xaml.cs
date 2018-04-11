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

        string image_Home = @"../Images/Home.png";
        string image_FolderClosed = @"../Images/FolderClosed.png";
        string image_FolderOpened = @"../Images/FolderOpened.png";
        string image_FolderSelected = @"../Images/FolderSelected.png";
        string image_DocumentSelected = @"../Images/DocumentSelected.png";
        string image_DocumentClosed = @"../Images/DocumentClosed.png";

        public static TreeViewItem selectedTVI = null;

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
            TreeViewModel.SetItemImageName(tviRoot, image_Home);
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
                    TreeViewModel.SetItemImageName(chldNode, image_DocumentClosed);
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
                    TreeViewModel.SetItemImageName(chldNode, image_FolderClosed);
                    TreeViewModel.SetItemTypeName(chldNode, ATConfig.TreeNodeType.FolderNode.ToString());
                    tviRoot.Items.Add(chldNode);
                    GetSolutionTree(chldFolder.FullName, chldNode);
                }
            }
           }

        private void ChldNode_Collapsed(object sender, RoutedEventArgs e)
        {
            var tviSender = e.OriginalSource as TreeViewItem;
            SetItemStyles(tviSender, image_FolderClosed);
        }

        private void ChldNode_Expanded(object sender, RoutedEventArgs e)
        {
            var tviSender = e.OriginalSource as TreeViewItem;
            SetItemStyles(tviSender, image_FolderOpened);
        }

        private void MySolutionTempView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            selectedTVI = (TreeViewItem)e.NewValue;
            SetItemStyles(selectedTVI, image_FolderSelected, image_DocumentSelected);
            TreeViewModel.SetMenuVisibility(selectedTVI, Visibility.Visible);
            if (e.OldValue != null)
            {
                var tviOld = (TreeViewItem)e.OldValue;
                if (TreeViewModel.GetItemTypeName(tviOld) == ATConfig.TreeNodeType.FolderNode.ToString() && tviOld.IsExpanded == true)
                {
                    SetItemStyles(tviOld, image_FolderOpened);
                }             
                else
                {
                    SetItemStyles(tviOld, image_FolderClosed, image_DocumentClosed);
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
            MessageBox.Show(selectedTVI.Tag.ToString());
        }

        private void MySolutionTempView_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        /// <summary>
        /// 定位对应文件夹下的文件
        /// </summary>
        /// <param name="fileFullName"></param>
        private void OpenFolderAndSelectFile(String fileFullName)
        {
            System.Threading.Thread.Sleep(30);
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + fileFullName;
            System.Diagnostics.Process.Start(psi);
        }

        private void MenuItem_OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderAndSelectFile(selectedTVI.Tag.ToString());
        }
    }
}
