using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace AutoDog
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private static NotifyIcon trayIcon;

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            RemoveTrayIcon();
            AddTrayIcon();
        }

        private void AddTrayIcon()
        {
            if (trayIcon != null)
            {
                return;
            }
            trayIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon("../../Images/AutoDog.ico"),
                Text = "测试管理平台"
            };
            trayIcon.Visible = true;

            ContextMenu menu = new ContextMenu();

            MenuItem aboutItem = new MenuItem() { Text = "关于" };
            MenuItem closeItem = new MenuItem() { Text = "退出" };
            closeItem.Click += new EventHandler(delegate { this.Shutdown(); });
            MenuItem loginItem = new MenuItem() { Text = "登录" };
            MenuItem loginOutItem = new MenuItem() { Text = "注销" };
            MenuItem showMainItem = new MenuItem() { Text = "显示主窗口" };
            showMainItem.Click += showMainItem_Click;

            menu.MenuItems.AddRange(new MenuItem[] { showMainItem, loginOutItem, loginItem, closeItem, aboutItem });

            trayIcon.ContextMenu = menu;    //设置NotifyIcon的右键弹出菜单
        }

        private void RemoveTrayIcon()
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            RemoveTrayIcon();
        }
        private void showMainItem_Click(object sender, EventArgs e)
        {
            if (this.MainWindow == null) MainWindow = new MainWindow();
            this.MainWindow.Show();
        }
    }
}
