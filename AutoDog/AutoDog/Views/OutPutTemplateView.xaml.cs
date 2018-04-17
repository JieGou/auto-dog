using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
        System.Diagnostics.Process CmdP = new System.Diagnostics.Process();
        ProcessPiper pp = new ProcessPiper();
        public OutPutTemplateView()
        {
            InitializeComponent();
            this.Loaded += OutPutTemplateView_Loaded;
        }

        private void OutPutTemplateView_Loaded(object sender, RoutedEventArgs e)
        {          
            this.outPutView.Text = pp.StdOut;
        }

        private void outPutView_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void outPutView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                CmdP.StandardInput.WriteLine(outPutView.Text + "&exit");
            }
        }

        private void InitCmdWindow()
        {
            CmdP.StartInfo.FileName = "Cmd.exe";
            CmdP.StartInfo.UseShellExecute = false;
            CmdP.StartInfo.RedirectStandardInput = true;
            CmdP.StartInfo.RedirectStandardOutput = true;
            CmdP.StartInfo.RedirectStandardError = true;
            CmdP.StartInfo.CreateNoWindow = false;     

            CmdP.EnableRaisingEvents = true;
            if (workSpace!=null) CmdP.StartInfo.WorkingDirectory = workSpace;
            CmdP.Start();
            CmdP.StandardInput.AutoFlush = true;
            CmdP.Exited += new EventHandler(RunAction_Exited);     
            CmdP.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            CmdP.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            this.outPutView.Text = CmdP.StandardOutput.ToString();
        }
        private void RunAction_Exited(object sender, EventArgs e)
        {
            
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
               
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                
            }
        }

        public class ProcessPiper
        {
            public string StdOut { get; private set; }
            public string StdErr { get; private set; }
            public string ExMessage { get; set; }
            public void Start(FileInfo exe, string args, Action<ProcessPiper> onComplete)
            {
                ProcessStartInfo psi = new ProcessStartInfo(exe.FullName, args);
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.WorkingDirectory = System.IO.Path.GetDirectoryName(exe.FullName);
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        ExMessage = string.Empty;
                        Process process = new Process();
                        process.StartInfo = psi;
                        process.Start();
                        process.WaitForExit();
                        StdOut = process.StandardOutput.ReadToEnd();
                        StdErr = process.StandardError.ReadToEnd();
                        onComplete(this);
                    }
                    catch (Exception ex)
                    {
                        ExMessage = ex.Message;
                    }
                });
            }
        }
    }
}
