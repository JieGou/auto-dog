using System.Windows.Forms;
using AutoDog.Windows;

namespace AutoDog.Views
{
    /// <summary>
    /// OutPutTemplateTree.xaml 的交互逻辑
    /// </summary>
    public partial class OutPutTemplateView : System.Windows.Controls.UserControl
    {
        public OutPutTemplateView()
        {
            InitializeComponent();
            //  Handle certain commands.
            viewModel.StartCommandPromptCommand.Executed += new Apex.MVVM.CommandEventHandler(StartCommandPromptCommand_Executed);
            viewModel.StartNewProcessCommand.Executed += new Apex.MVVM.CommandEventHandler(StartNewProcessCommand_Executed);
            viewModel.StopProcessCommand.Executed += new Apex.MVVM.CommandEventHandler(StopProcessCommand_Executed);
            viewModel.ClearOutputCommand.Executed += new Apex.MVVM.CommandEventHandler(ClearOutputCommand_Executed);
        }

        void StartCommandPromptCommand_Executed(object sender, Apex.MVVM.CommandEventArgs args)
        {
            terminalControl.StartProcess("cmd.exe", string.Empty);
            UpdateProcessState();
        }

        void StartNewProcessCommand_Executed(object sender, Apex.MVVM.CommandEventArgs args)
        {
            //  Create the new process form.
            FormNewProcess formNewProcess = new FormNewProcess();

            //  If the form is shown OK, start the process.
            if (formNewProcess.ShowDialog() == DialogResult.OK)
            {
                //  Start the proces.
                terminalControl.StartProcess(formNewProcess.FileName, formNewProcess.Arguments);

                //  Update the UI state.
                UpdateProcessState();
            }
        }

        void StopProcessCommand_Executed(object sender, Apex.MVVM.CommandEventArgs args)
        {
            terminalControl.StopProcess();
            UpdateProcessState();
        }

        void ClearOutputCommand_Executed(object sender, Apex.MVVM.CommandEventArgs args)
        {
            terminalControl.ClearOutput();
        }

        private void UpdateProcessState()
        {
            //  Update the state.
            if (terminalControl.IsProcessRunning)
                viewModel.ProcessState = "Running " + System.IO.Path.GetFileName(terminalControl.ProcessInterface.ProcessFileName);
            else
                viewModel.ProcessState = "Not Running";
        }
    }
}
