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
using TestExerciserPro.TEViews.AutoTesting;

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

            MySolutionTempView.Items.Add(tviRoot);
        }

        private void GetSolutionTree(string filePath,TreeViewItem tviRoot)
        {           
            try
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
                        TreeViewModel.SetItemImageName(chldNode, @"../Images/Document.png");
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
                        TreeViewModel.SetItemImageName(chldNode, @"../Images/FolderClosed.png");
                        tviRoot.Items.Add(chldNode);
                        GetSolutionTree(chldFolder.FullName, chldNode);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
