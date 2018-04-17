using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoDog.Logic;
using System.Windows.Threading;
using System.Diagnostics;

namespace AutoDog.Views
{
    /// <summary>
    /// OutPutTemplateTree.xaml 的交互逻辑
    /// </summary>
    public partial class OutPutTemplateView : UserControl
    {
        string workSpace = SolutionTemplateView.currentSolutionPath;
        int cmdOriginal = 0;
        System.Diagnostics.Process cmdP = new System.Diagnostics.Process();
        public OutPutTemplateView()
        {
            InitializeComponent();
            InitCmdWindow();
            outPutView.AppendText(cmdP.StandardOutput.ReadLine() + "\r");
            outPutView.AppendText(cmdP.StandardOutput.ReadLine() + "\r");
            outPutView.AppendText(cmdP.StartInfo.WorkingDirectory + ">");
            cmdOriginal = outPutView.Text.Length;
            outPutView.TextChanged += outPutView_TextChanged;
        }
        private void outPutView_TextChanged(object sender, TextChangedEventArgs e)
        {            
            if(outPutView.Text.Length<=cmdOriginal)
            {
                e.Changes.Clear();
            }  
        }

        private void outPutView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cmdP.StandardInput.WriteLine("cmd.exe", "&exit");
            }
        }

        private void InitCmdWindow()
        {
            cmdP.StartInfo.FileName = "Cmd.exe";
            cmdP.StartInfo.UseShellExecute = false;
            cmdP.StartInfo.RedirectStandardInput = true;
            cmdP.StartInfo.RedirectStandardOutput = true;
            cmdP.StartInfo.RedirectStandardError = true;
            cmdP.StartInfo.CreateNoWindow = true;     
            cmdP.EnableRaisingEvents = true;
            if (workSpace != null)
            { cmdP.StartInfo.WorkingDirectory = workSpace; }
            else
            {
                cmdP.StartInfo.WorkingDirectory =System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            }
            cmdP.Start();
            cmdP.StandardInput.WriteLine("&exit");
            cmdP.Exited += new EventHandler(RunAction_Exited);     
            cmdP.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            cmdP.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
        }
        private void RunAction_Exited(object sender, EventArgs e)
        {
            
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                outPutView.AppendText(e.Data);
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                outPutView.AppendText(e.Data);
            }
        }
    }
}
