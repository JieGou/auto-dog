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
using System.Text.RegularExpressions;
using AutoDog.ViewModels;
using AutoDog.Logics;
using AutoDog.UI.Controls;
using AutoDog.UI.Controls.Dialogs;

namespace AutoDog.Views
{
    /// <summary>
    /// TreeSolution.xaml 的交互逻辑
    /// </summary>
    public partial class SolutionTemplateView : UserControl
    {

        string image_Solution = @"../../Images/DockPanel/SolutionView_32.png";
        string image_FolderClosed = @"../../Images/FolderClosed.png";
        string image_FolderOpened = @"../../Images/FolderOpened.png";
        string image_FolderSelected = @"../../Images/FolderSelected.png";
        string image_DocumentSelected = @"../../Images/DocumentSelected.png";
        string image_DocumentClosed = @"../../Images/DocumentClosed.png";

        public static TreeViewItem selectedTVI = null;
        public static TextBox selectedTBox = null;
        public static string currentSolutionPath = null;

        public SolutionTemplateView()
        {
            InitializeComponent();
            mySTVInit();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "solutionPath") { mySTVInit(); }
        }

        public void mySTVInit()
        {
            string rootPath = Properties.Settings.Default.solutionPath;
            if (Directory.Exists(rootPath))
            {
                DirectoryInfo rootFolder = new DirectoryInfo(rootPath);
                if(rootFolder.Exists)
                {
                    //设置树根节点
                    var tviRoot = new TreeViewItem();
                    tviRoot.Header = rootFolder.Name;
                    tviRoot.Tag = rootFolder.FullName;
                    TreeViewModelRef.SetItemImageName(tviRoot, image_Solution);
                    TreeViewModelRef.SetItemTypeName(tviRoot, ATConfig.TreeNodeType.RootNode.ToString());
                    GetSolutionTree(tviRoot, rootFolder);
                    mySTV.Items.Add(tviRoot);
                }
            }           
        }

        private void RefreshTreeNode(TreeViewItem tviRoot)
        {
            tviRoot.Items.Clear();
            GetSolutionTree(tviRoot,new DirectoryInfo(tviRoot.Tag.ToString()));
        }

        private void SetFileNode(TreeViewItem tviRoot,DirectoryInfo rootFolder)
        {
            //设置树文件节点
            FileInfo[] chldFiles = rootFolder.GetFiles("*.*");
            if (chldFiles != null)
            {
                foreach (FileInfo chlFile in chldFiles)
                {
                    TreeViewItem chldNode = new TreeViewItem();
                    chldNode.Header = chlFile.Name;
                    chldNode.Tag = chlFile.FullName;
                    AddTreeViewItem(chldNode, image_DocumentClosed, ATConfig.TreeNodeType.FileNode.ToString(), false);
                    //string ext = chlFile.Name.Substring(chlFile.Name.LastIndexOf(".") + 1, (chlFile.Name.Length - chlFile.Name.LastIndexOf(".") - 1));
                    tviRoot.Items.Add(chldNode);
                }
            }
        }

        private void SetFolderNode(TreeViewItem tviRoot, DirectoryInfo rootFolder)
        {
            //设置树文件夹节点
            DirectoryInfo[] chldFolders = rootFolder.GetDirectories();
            if (chldFolders != null)
            {
                foreach (DirectoryInfo chldFolder in chldFolders)
                {
                    TreeViewItem chldNode = new TreeViewItem();
                    chldNode.Header = chldFolder.Name;
                    chldNode.Tag = chldFolder.FullName;
                    chldNode.Expanded += ChldNode_Expanded;
                    chldNode.Collapsed += ChldNode_Collapsed;
                    AddTreeViewItem(chldNode, image_FolderClosed, ATConfig.TreeNodeType.FolderNode.ToString(), false);

                    tviRoot.Items.Add(chldNode);
                    GetSolutionTree(chldNode, chldFolder);
                }
            }
        }

        private void GetSolutionTree(TreeViewItem tviRoot, DirectoryInfo rootFolder)
        {
            SetFileNode(tviRoot, rootFolder);
            SetFolderNode(tviRoot, rootFolder);
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
            this.mySTVContextMenu.Visibility = Visibility.Visible;
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
                currentSolutionPath =tvi.Tag.ToString();
                UpdateRootMenuItem();
            }
            if (TreeViewModelRef.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FolderNode.ToString())
            {
                TreeViewModelRef.SetItemImageName(tvi, image_FolderSelected);
                UpdateFolderMenuItem();
            }
            else if (TreeViewModelRef.GetItemTypeName(tvi) == ATConfig.TreeNodeType.FileNode.ToString())
            {
                TreeViewModelRef.SetItemImageName(tvi, image_DocumentSelected);
                UpdateFileMenuItem();
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
            if (selectedTVI != null && TreeViewModelRef.GetItemTypeName(selectedTVI)== ATConfig.TreeNodeType.FileNode.ToString())
            {               
                var fileViewModel = Workspace.This.Open(selectedTVI.Tag.ToString());
                Workspace.This.ActiveDocument = fileViewModel;
            }
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
            renameMethod();
        }
        private void mySTVMenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            if(TreeViewModelRef.GetItemTypeName(selectedTVI) == ATConfig.TreeNodeType.FileNode.ToString())
            {
                File.Delete(selectedTVI.Tag.ToString());
                RefreshTreeNode((TreeViewItem)selectedTVI.Parent);
            }
            if(TreeViewModelRef.GetItemTypeName(selectedTVI) == ATConfig.TreeNodeType.FolderNode.ToString())
            {
                ShowDeleteFolderDialog();
            }
        }

        private void mySTV_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedTVI != null)
            {
                if (e.Key == Key.F2)
                {
                    renameMethod();
                }
            }
        }

        private void renameMethod()
        {
            TreeViewModelRef.SetIsEditMode(selectedTVI, true);
            selectedTBox.Focus();
            selectedTBox.SelectAll();
            oldText = selectedTBox.Text;
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
            {
                string oldPath = selectedTVI.Tag.ToString();
                selectedTVI.Header = selectedTBox.Text;
                selectedTVI.Tag = Regex.Match(oldPath, @"[\S\s]*\\").Value + selectedTVI.Header;
                string newPath = selectedTVI.Tag.ToString();              
                MoveFile(oldPath, newPath);
                RefreshTreeNode(selectedTVI);
                TreeViewModelRef.SetIsEditMode(selectedTVI, false);
            }
            if (e.Key == Key.Escape)
            {
                selectedTBox.Text = oldText;
                TreeViewModelRef.SetIsEditMode(selectedTVI, false);
            }
        }


        private void MoveFile(string oldPath,string newPath)
        {
            FileInfo fileToMove = new FileInfo(oldPath);
            fileToMove.MoveTo(newPath);
        }

        private static void MoveFolder(string oldPath, string newPath)
        {
            if (Directory.Exists(oldPath))
            {
                if (!Directory.Exists(newPath))
                {
                    //目标目录不存在则创建  
                    try
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件  
                List<string> files = new List<string>(Directory.GetFiles(oldPath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { newPath, Path.GetFileName(c) });
                    //覆盖模式  
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }
                    File.Move(c, destFile);
                });
                //获得源文件下所有目录文件  
                List<string> folders = new List<string>(Directory.GetDirectories(oldPath));

                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { newPath, Path.GetFileName(c) });
                    //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。  
                    //Directory.Move(c, destDir);  

                    //采用递归的方法实现  
                    MoveFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }

        private void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("删除目标目录失败：" + ex.Message);
            }
        }


        private void UpdateRootMenuItem()
        {
            mySTVMenuAdd.IsEnabled = true;
            mySTVMenuOpen.IsEnabled = false;
            mySTVMenuDelete.IsEnabled = false;
        }

        private void UpdateFolderMenuItem()
        {
            mySTVMenuAdd.IsEnabled = true;
            mySTVMenuOpen.IsEnabled = false;
            mySTVMenuDelete.IsEnabled = true;
        }
        private void UpdateFileMenuItem()
        {
            mySTVMenuAdd.IsEnabled = false;
            mySTVMenuOpen.IsEnabled = true;
            mySTVMenuDelete.IsEnabled = true;
        }

        private async void ShowDeleteFolderDialog()
        {
            MetroWindow window = MainWindow.metroWindow;
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                ColorScheme = window.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await window.ShowMessageAsync("提示信息", "确定要删除当前文件夹吗？",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative) DeleteFodler();
            if (result == MessageDialogResult.Negative) return;
        }

        private void DeleteFodler()
        {
            DelectDir(selectedTVI.Tag.ToString());
            Directory.Delete(selectedTVI.Tag.ToString());
            RefreshTreeNode((TreeViewItem)selectedTVI.Parent);
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
    }
}
