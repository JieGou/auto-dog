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
        }

        private readonly object _dummyNode = null;

        // The main loader, in this sample app it is always "LoadSubItems"
        // RUNS ON:  Background Thread
        delegate void DEL_Loader(TreeViewItem tviLoad, string strPath, DEL_GetItems actGetItems, DEL_AddSubItem actAddSubItem);

        // Adds the actual TreeViewItem, in this sample it's either "AddFolderItem" or "AddDriveItem"
        // RUNS ON:  UI Thread
        delegate void DEL_AddSubItem(TreeViewItem tviParent, string strPath);

        // Gets an IEnumerable for the items to load, in this sample it's either "GetFolders" or "GetDrives"
        // RUNS ON:  Background Thread
        delegate IEnumerable<string> DEL_GetItems(string strParent);

       public void SolutionTemplateViewInit()
        {
            // Create a new TreeViewItem to serve as the root.
            var tviRoot = new TreeViewItem();

            // Set the header to display the text of the item.
            tviRoot.Header = "我的电脑";

            // Add a dummy node so the 'plus' indicator
            // shows in the tree
            //getFiles(MainAutoTesting.solutionPath,tviRoot);
            getFiles(@"F:\自动化测试", tviRoot);
            //tviRoot.Items.Add(_dummyNode);

            // Set the item expand handler
            // This is where the defered loading is handled
            //tviRoot.Expanded += OnFolder_Expanded;

            // Set the attached property 'ItemImageName'	// to the image we want displayed in the tree
            TreeViewItemProps.SetItemImageName(tviRoot, @"../Images/Computer.png");

            // Add the item to the tree	folders
            MySolutionTempView.Items.Add(tviRoot);
        }


        private void getFiles(string filePath, TreeViewItem tvi)
        {
            try
            {
                if (filePath == null || filePath == "")
                {

                }
                else
                {
                    DirectoryInfo folder = new DirectoryInfo(filePath);
                    tvi.Header = folder.Name;
                    tvi.Tag = folder.FullName;
                    FileInfo[] chldFiles = folder.GetFiles("*.*");
                    foreach (FileInfo chlFile in chldFiles)
                    {
                        TreeViewItem chldNode = new TreeViewItem();
                        chldNode.Header = chlFile.Name;
                        chldNode.Tag = chlFile.FullName;
                        string ext = chlFile.Name.Substring(chlFile.Name.LastIndexOf(".") + 1, (chlFile.Name.Length - chlFile.Name.LastIndexOf(".") - 1));
                        tvi.Items.Add(chldNode);
                    }

                    DirectoryInfo[] chldFolders = folder.GetDirectories();
                    foreach (DirectoryInfo chldFolder in chldFolders)
                    {
                        TreeViewItem chldNode = new TreeViewItem();
                        chldNode.Header = folder.Name;
                        chldNode.Tag = folder.FullName;
                        tvi.Items.Add(chldNode);
                        getFiles(chldFolder.FullName, chldNode);
                    }
                }
            }
            catch (Exception exception)
            {

            }
        }

        bool IsItemNotLoaded(TreeViewItem tviSender)
        {
            if (tviSender != null)
            {
                return (TreeViewItemProps.GetIsLoaded(tviSender) == false);
            }
            return (false);
        }

        void OnFolder_Expanded(object sender, RoutedEventArgs e)
        {
            var tviSender = e.OriginalSource as TreeViewItem;
            if (IsItemNotLoaded(tviSender))
            {
                e.Handled = true;
                StartItemLoading(tviSender, GetFolders, AddFolderItem);
            }
        }

        void StartItemLoading(TreeViewItem tviSender, DEL_GetItems actGetItems, DEL_AddSubItem actAddSubItem)
        {
            // Add a entry in the cancel state dictionary
            SetCancelState(tviSender, false);

            // Clear away the dummy node
            tviSender.Items.Clear();

            // Set all attached props to their proper default values
            // This causes the progress bar and cancel button to appear
            TreeViewItemProps.SetIsCanceled(tviSender, false);
            TreeViewItemProps.SetIsLoaded(tviSender, true);
            TreeViewItemProps.SetIsLoading(tviSender, true);

            // Store a ref to the main loader logic for cleanup purposes
            DEL_Loader actLoad = LoadSubItems;

            // Invoke the loader on a background thread.
            actLoad.BeginInvoke(tviSender, tviSender.Tag as string, actGetItems, actAddSubItem, ProcessAsyncCallback, actLoad);
        }

        // Keeps a list of all TreeViewItems currently expanding.
        // If a cancel request comes, it cause the bool value to be set to true.
        Dictionary<TreeViewItem, bool> m_dic_ItemsExecuting = new Dictionary<TreeViewItem, bool>();

        // Set's the cancel state of specific TreeViewItem
        void SetCancelState(TreeViewItem tviSender, bool bState)
        {
            lock (m_dic_ItemsExecuting)
            {
                m_dic_ItemsExecuting[tviSender] = bState;
            }
        }

        // Get's the cancel state of specific TreeViewItem
        bool GetCancelState(TreeViewItem tviSender)
        {
            lock (m_dic_ItemsExecuting)
            {
                bool bState = false;
                m_dic_ItemsExecuting.TryGetValue(tviSender, out bState);
                return (bState);
            }
        }

        // Remove's the TreeViewItem from the cancel dictionary
        void RemoveCancel(TreeViewItem tviSender)
        {
            lock (m_dic_ItemsExecuting)
            {
                m_dic_ItemsExecuting.Remove(tviSender);
            }
        }


        void ResetTreeItem(TreeViewItem tviIn, bool bClear)
        {
            if (bClear)
            {
                tviIn.Items.Clear();
                tviIn.Items.Add(_dummyNode);
                tviIn.IsExpanded = false;
            }
            TreeViewItemProps.SetIsLoaded(tviIn, false);
        }

        // Amount of delay for each item in this demo
        static private double sm_dbl_ItemDelayInSeconds = 0.3;

        // Runs on background thread.
        // Queuing updates can help in rapid loading scenerios,
        // I just wanted to illustrate a more granualar approach.
        void LoadSubItems(TreeViewItem tviParent, string strPath, DEL_GetItems actGetItems, DEL_AddSubItem actAddSubItem)
        {
            try
            {
                foreach (string dir in actGetItems(strPath))
                {
                    // Be really slow :) for demo purposes
                    Thread.Sleep(TimeSpan.FromSeconds(sm_dbl_ItemDelayInSeconds).Milliseconds);

                    // Check to see if cancel is requested
                    if (GetCancelState(tviParent))
                    {
                        // If cancel dispatch "ResetTreeItem" for the parent node and
                        // get out of here.
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => ResetTreeItem(tviParent, false)));
                        break;
                    }
                    else
                    {
                        // Call "actAddSubItem" on the UI thread to create a TreeViewItem and add it the control.
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, actAddSubItem, tviParent, dir);
                    }
                }
            }
            catch (Exception ex)
            {
                // Reset the TreeViewItem to unloaded state if an exception occurs
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => ResetTreeItem(tviParent, true)));

                // Rethrow any exceptions, the EndInvoke handler "ProcessAsyncCallback" 
                // will redispatch on UI thread for further processing and notification.
                throw ex;
            }
            finally
            {
                // Ensure the TreeViewItem is no longer in the cancel state dictionary.
                RemoveCancel(tviParent);

                // Set the "IsLoading" dependency property is set to 'false'
                // this will cause all loading UI (i.e. progress bar, cancel button) to disappear.
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => TreeViewItemProps.SetIsLoading(tviParent, false)));
            }
        }


        // Runs on Background thread.
        IEnumerable<string> GetFolders(string strParent)
        {
            return (Directory.GetDirectories(MainAutoTesting.solutionPath));
        }

        IEnumerable<string> GetFiles(string strParent)
        {
            return (Directory.GetFiles(strParent));
        }

        // Runs on UI thread.
        void AddFolderItem(TreeViewItem tviParent, string strPath)
        {
            IntAddItem(tviParent, System.IO.Path.GetFileName(strPath), strPath, @"Images/Folder.png");
        }

        // Runs on UI thread.
        void AddDriveItem(TreeViewItem tviParent, string strPath)
        {
            IntAddItem(tviParent, strPath, strPath, @"Images/DiskDrive.png");
        }

        private void IntAddItem(TreeViewItem tviParent, string strName, string strTag, string strImageName)
        {
            var tviSubItem = new TreeViewItem();
            tviSubItem.Header = strName;
            tviSubItem.Tag = strTag;
            tviSubItem.Items.Add(_dummyNode);
            tviSubItem.Expanded += OnFolder_Expanded;

            TreeViewItemProps.SetItemImageName(tviSubItem, strImageName);
            TreeViewItemProps.SetIsLoading(tviSubItem, false);

            tviParent.Items.Add(tviSubItem);
        }

        private void ProcessAsyncCallback(IAsyncResult iAR)
        {
            // Call end invoke on UI thread to process any exceptions, etc.
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() => ProcessEndInvoke(iAR)));
        }

        private void ProcessEndInvoke(IAsyncResult iAR)
        {
            try
            {
                var actInvoked = (DEL_Loader)iAR.AsyncState;
                actInvoked.EndInvoke(iAR);
            }
            catch (Exception ex)
            {
                // Probably should check for useful inner exceptions
                MessageBox.Show(string.Format("Error in ProcessEndInvoke\r\nException:  {0}", ex.Message));
            }
        }

        // When the cancel button is clicked, I get access to 
        // the TreeViewItem in question from the button's Tag property,
        // which I set a binding for in the XAML.
        // Then I mark the TreeViewItem as canceling.
        private void btnCancelLoad_Click(object sender, RoutedEventArgs e)
        {
            var btnSend = e.OriginalSource as Button;
            if (btnSend != null)
            {
                var tviOwner = btnSend.Tag as TreeViewItem;
                if (tviOwner != null)
                {
                    TreeViewItemProps.SetIsCanceled(tviOwner, true);
                    lock (m_dic_ItemsExecuting)
                    {
                        if (m_dic_ItemsExecuting.ContainsKey(tviOwner))
                        {
                            m_dic_ItemsExecuting[tviOwner] = true;
                        }
                    }
                }
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            var btnSend = e.OriginalSource as Button;
            if (btnSend != null)
            {
                var tviOwner = btnSend.Tag as TreeViewItem;
                if (tviOwner != null)
                {
                    tviOwner.IsExpanded = false;
                    tviOwner.IsExpanded = true;
                }
            }
        }
    }
}
