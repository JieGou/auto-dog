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
using AutoDog.ViewModels;
using AutoDog.Logics;

namespace AutoDog.Views
{
    /// <summary>
    /// ResourcesTemplateView.xaml 的交互逻辑
    /// </summary>
    public partial class ResourcesTemplateView : UserControl
    {
        public ResourcesTemplateView()
        {
            InitializeComponent();
            ResourcesTemplateViewInit();
        }

        private readonly object _dummyNode = null;

        //后台线程
        delegate void DelegateLoader(TreeViewItem tviLoad, string strPath, DelegateGetItems actGetItems, DelegateAddSubItem actAddSubItem);

        //UI线程
        delegate void DelegateAddSubItem(TreeViewItem tviParent, string strPath);

        //后台线程
        delegate IEnumerable<string> DelegateGetItems(string strParent);

        public void ResourcesTemplateViewInit()
        {
            var tviRoot = new TreeViewItem();

            tviRoot.Header = "我的电脑";

            tviRoot.Items.Add(_dummyNode);

            tviRoot.Expanded += OnRoot_Expanded;

            TreeViewModelRef.SetItemImageName(tviRoot, @"../../Images/DockPanel/ResourceView_32.png");

            myResourcesTree.Items.Add(tviRoot);
        }

        void OnRoot_Expanded(object sender, RoutedEventArgs e)
        {
            var tviSender = e.OriginalSource as TreeViewItem;
            if (IsItemNotLoaded(tviSender))
            {
                StartItemLoading(tviSender, GetDrives, AddDriveItem);
            }
        }

        bool IsItemNotLoaded(TreeViewItem tviSender)
        {
            if (tviSender != null)
            {
                return (TreeViewModelRef.GetIsLoaded(tviSender) == false);
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
                StartItemLoading(tviSender, GetFiles, AddFileItem);
            }
        }

        void StartItemLoading(TreeViewItem tviSender, DelegateGetItems actGetItems, DelegateAddSubItem actAddSubItem)
        {
            SetCancelState(tviSender, false);

            tviSender.Items.Clear();

            TreeViewModelRef.SetIsCanceled(tviSender, false);
            TreeViewModelRef.SetIsLoaded(tviSender, true);
            TreeViewModelRef.SetIsLoading(tviSender, true);

            DelegateLoader actLoad = LoadSubItems;

            // 调用Load事件
            actLoad.BeginInvoke(tviSender, tviSender.Tag as string, actGetItems, actAddSubItem, ProcessAsyncCallback, actLoad);
        }

        // 保存当前正在扩展的所有treeviewitem的列表。
        // 如果一个cancel请求出现，它会导致bool值被设置为true。
        Dictionary<TreeViewItem, bool> m_dic_ItemsExecuting = new Dictionary<TreeViewItem, bool>();

        // 设置取消状态
        void SetCancelState(TreeViewItem tviSender, bool bState)
        {
            lock (m_dic_ItemsExecuting)
            {
                m_dic_ItemsExecuting[tviSender] = bState;
            }
        }

        // 获取取消状态
        bool GetCancelState(TreeViewItem tviSender)
        {
            lock (m_dic_ItemsExecuting)
            {
                bool bState = false;
                m_dic_ItemsExecuting.TryGetValue(tviSender, out bState);
                return (bState);
            }
        }

        // 从字典中删除TreeViewItem
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
            TreeViewModelRef.SetIsLoaded(tviIn, false);
        }

        // 设置延迟时间
        //static private double sm_dbl_ItemDelayInSeconds = 0.3;

        void LoadSubItems(TreeViewItem tviParent, string strPath, DelegateGetItems actGetItems, DelegateAddSubItem actAddSubItem)
        {
            try
            {
                foreach (string dir in actGetItems(strPath))
                {
                    // 设置延迟
                    //Thread.Sleep(TimeSpan.FromSeconds(sm_dbl_ItemDelayInSeconds).Milliseconds);

                    // 检查取消是否被访问
                    if (GetCancelState(tviParent))
                    {
                        // 如果取消为父节点发送“ResetTreeItem”。
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => ResetTreeItem(tviParent, false)));
                        break;
                    }
                    else
                    {
                        //在UI线程上调用“actAddSubItem”来创建TreeViewItem并添加它的控件。
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, actAddSubItem, tviParent, dir);
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => ResetTreeItem(tviParent, true)));
                //throw ex;
            }
            finally
            {
                // 确保TreeViewItem不再在取消状态中
                RemoveCancel(tviParent);

                // 设置“IsLoading”依赖性属性设置为“false”
                // 所有加载UI（例如进度条、取消按钮）消失。
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => TreeViewModelRef.SetIsLoading(tviParent, false)));
            }
        }

        // 后台线程
        IEnumerable<string> GetFiles(string strParent)
        {
            return (Directory.GetFiles(strParent));
        }

        // 后台线程
        IEnumerable<string> GetFolders(string strParent)
        {
            return (Directory.GetDirectories(strParent));
        }

        // 后台线程
        IEnumerable<string> GetDrives(string strParent)
        {
            return (Directory.GetLogicalDrives());
        }

        // UI线程
        void AddFolderItem(TreeViewItem tviParent, string strPath)
        {
            IntAddItem(tviParent, System.IO.Path.GetFileName(strPath), strPath, @"../../Images/WinFolder.gif");
        }

        void AddFileItem(TreeViewItem tviParent, string strPath)
        {
            IntAddItem(tviParent, System.IO.Path.GetFileName(strPath), strPath, @"../../Images/DocumentClosed.png");
        }

        // UI线程
        void AddDriveItem(TreeViewItem tviParent, string strPath)
        {
            IntAddItem(tviParent, strPath, strPath, @"../../Images/DiskDrive.png");
        }

        private void IntAddItem(TreeViewItem tviParent, string strName, string strTag, string strImageName)
        {
            var tviSubItem = new TreeViewItem();
            tviSubItem.Header = strName;
            tviSubItem.Tag = strTag;
            tviSubItem.Items.Add(_dummyNode);
            tviSubItem.Expanded += OnFolder_Expanded;


            TreeViewModelRef.SetItemImageName(tviSubItem, strImageName);
            TreeViewModelRef.SetIsLoading(tviSubItem, false);

            tviParent.Items.Add(tviSubItem);
        }

        private void ProcessAsyncCallback(IAsyncResult iAR)
        {
            // 在UI线程上调用end invoke来处理任何异常，等等。
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() => ProcessEndInvoke(iAR)));
        }

        private void ProcessEndInvoke(IAsyncResult iAR)
        {
            try
            {
                var actInvoked = (DelegateLoader)iAR.AsyncState;
                actInvoked.EndInvoke(iAR);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error in ProcessEndInvoke\r\nException:  {0}", ex.Message));
            }
        }

        //当“取消”按钮被点击时，根据按钮的Tag属性来标记取消状态为正在取消
        private void btnCancelLoad_Click(object sender, RoutedEventArgs e)
        {
            var btnSend = e.OriginalSource as Button;
            if (btnSend != null)
            {
                var tviOwner = btnSend.Tag as TreeViewItem;
                if (tviOwner != null)
                {
                    TreeViewModelRef.SetIsCanceled(tviOwner, true);
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
