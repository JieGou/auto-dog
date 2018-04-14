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
        public static TextBox selectedTBox = null;

        public SolutionTemplateView()
        {
            InitializeComponent();
            mySTVInit();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            mySTVInit();
        }

        public void mySTVInit()
        {
            var tviRoot = new TreeViewItem();

            GetSolutionTree(Properties.Settings.Default.solutionPath,tviRoot);
            TreeViewModelRef.SetItemImageName(tviRoot, image_Home);
            TreeViewModelRef.SetItemTypeName(tviRoot,ATConfig.TreeNodeType.RootNode.ToString());
            mySTV.Items.Add(tviRoot);
        }

        private void GetSolutionTree(string rootPath,TreeViewItem tviRoot)
        {
            if (rootPath != null && rootPath != "")
            {
                //设置树根节点
                DirectoryInfo rootFolder = new DirectoryInfo(rootPath);
                tviRoot.Header = rootFolder.Name;
                tviRoot.Tag = rootFolder.FullName;

                //设置树文件节点
                FileInfo[] chldFiles = rootFolder.GetFiles("*.*");
                foreach (FileInfo chlFile in chldFiles)
                {
                    TreeViewItem chldNode = new TreeViewItem();
                    chldNode.Header = chlFile.Name;
                    chldNode.Tag = chlFile.FullName;
                    AddTreeViewItem(chldNode, image_DocumentClosed, ATConfig.TreeNodeType.FileNode.ToString(), false);
                    //string ext = chlFile.Name.Substring(chlFile.Name.LastIndexOf(".") + 1, (chlFile.Name.Length - chlFile.Name.LastIndexOf(".") - 1));
                    tviRoot.Items.Add(chldNode);
                }

                //设置树文件夹节点
                DirectoryInfo[] chldFolders = rootFolder.GetDirectories();
                foreach (DirectoryInfo chldFolder in chldFolders)
                {
                    TreeViewItem chldNode = new TreeViewItem();
                    chldNode.Header = chldFolder.Name;
                    chldNode.Tag = chldFolder.FullName;
                    chldNode.Expanded += ChldNode_Expanded;
                    chldNode.Collapsed += ChldNode_Collapsed;
                    AddTreeViewItem(chldNode,image_FolderClosed, ATConfig.TreeNodeType.FolderNode.ToString(),false);

                    tviRoot.Items.Add(chldNode);
                    GetSolutionTree(chldFolder.FullName, chldNode);
                }
            }
           }

        private void AddTreeViewItem( TreeViewItem chldNode,string imageName,string typeName,bool editMode)
        {
            TreeViewModelRef.SetItemImageName(chldNode, imageName);
            TreeViewModelRef.SetItemTypeName(chldNode, typeName);
            TreeViewModelRef.SetIsEditMode(chldNode, editMode);
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

        private void mySTV_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {           
            selectedTVI = (TreeViewItem)e.NewValue;
            SetNewItemStyles(selectedTVI);
            if (e.OldValue != null)
            {
                var tviOld = (TreeViewItem)e.OldValue;
                if (TreeViewModelRef.GetItemTypeName(tviOld) == ATConfig.TreeNodeType.FolderNode.ToString() && tviOld.IsExpanded == true)
                {
                    SetItemStyles(tviOld, image_FolderOpened);
                }             
                else
                {
                    SetOldItemStyles(tviOld);
                }       
            }                         
        }

        void SetNewItemStyles(TreeViewItem tvi)
        {
            if (TreeViewModelRef.GetItemTypeName(tvi) == ATConfig.TreeNodeType.RootNode.ToString())
            {
                return;
            }
            if (TreeViewModelRef.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FolderNode.ToString())
            {
                TreeViewModelRef.SetItemImageName(tvi, image_FolderSelected);
            }
            else if (TreeViewModelRef.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FileNode.ToString())
            {
                TreeViewModelRef.SetItemImageName(tvi, image_DocumentSelected);
            }
        }


        void SetOldItemStyles(TreeViewItem tvi)
        {
            if (TreeViewModelRef.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FolderNode.ToString())
            {
                TreeViewModelRef.SetItemImageName(tvi, image_FolderClosed);
            }
            else if (TreeViewModelRef.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FileNode.ToString())
            {
                TreeViewModelRef.SetItemImageName(tvi, image_DocumentClosed);
            }
        }

        void SetItemStyles(TreeViewItem tvi, string folderImage)
        {
            if (TreeViewModelRef.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FolderNode.ToString())
            {
                TreeViewModelRef.SetItemImageName(tvi, folderImage);
            }
        }

        private void mySTV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //此处需要判断是否选中节点，防止未选中节点时，鼠标在空白区域点击后引发异常
            if (selectedTVI != null)
            {
                var fileViewModel = Workspace.This.Open(selectedTVI.Tag.ToString());
                Workspace.This.ActiveDocument = fileViewModel;
            }
        }

        private void mySTV_MouseWheel(object sender, MouseWheelEventArgs e)
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

        private void mySTVMenuItem_OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderAndSelectFile(selectedTVI.Tag.ToString());
        }

        private void mySTVMenuItem_ShowProperty_Click(object sender, RoutedEventArgs e)
        {
            Workspace.This.FileStats.IsSelected = true;
        }

        private void mySTVMenuItem_ReName_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void mySTVMenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void mySTV_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedTVI != null)
            {
                if (e.Key == Key.F2)
                {
                    TreeViewModelRef.SetIsEditMode(selectedTVI, true);
                    selectedTBox.Focus();
                    selectedTBox.SelectAll();
                    oldText = selectedTBox.Text;
                }
            }
        }

        string oldText;
        private void spTBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            selectedTBox = sender as TextBox;      
        }

        private void spTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TreeViewModelRef.SetIsEditMode(selectedTVI, false);
        }

        private void spTBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TreeViewModelRef.SetIsEditMode(selectedTVI, false);
            if (e.Key == Key.Escape)
            {
                selectedTBox.Text = oldText;
                TreeViewModelRef.SetIsEditMode(selectedTVI, false);
            }
        }
    }
}
