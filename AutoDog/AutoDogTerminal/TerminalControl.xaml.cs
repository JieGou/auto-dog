using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AutoDogTerminalAPI;

namespace AutoDogTerminal
{


    /// <summary>
    /// Interaction logic for ConsoleControl.xaml
    /// </summary>
    public partial class TerminalControl : UserControl
    {
        public TerminalControl()
        {
            InitializeComponent();

            processInterace.OnProcessOutput += processInterace_OnProcessOutput;
            processInterace.OnProcessError += processInterace_OnProcessError;
            processInterace.OnProcessInput += processInterace_OnProcessInput;
            processInterace.OnProcessExit += processInterace_OnProcessExit;

            richTextBoxConsole.PreviewKeyDown += richTextBoxConsole_KeyDown;
        }

        void processInterace_OnProcessError(object sender, ProcessEventArgs args)
        {
            WriteOutput(args.Content, Colors.Red);

            FireProcessOutputEvent(args);
        }

        void processInterace_OnProcessOutput(object sender, ProcessEventArgs args)
        {
            WriteOutput(args.Content, Colors.Black);

            FireProcessOutputEvent(args);
        }

        void processInterace_OnProcessInput(object sender, ProcessEventArgs args)
        {
            FireProcessInputEvent(args);
        }

        void processInterace_OnProcessExit(object sender, ProcessEventArgs args)
        {
            RunOnUIDespatcher(() =>
            {
                if (ShowDiagnostics)
                {
                    WriteOutput(Environment.NewLine + processInterace.ProcessFileName + " exited.", Color.FromArgb(255, 0, 255, 0));
                }

                richTextBoxConsole.IsReadOnly = true;
                IsProcessRunning = false;
            });
        }

        void richTextBoxConsole_KeyDown(object sender, KeyEventArgs e)
        {
            bool inReadOnlyZone = richTextBoxConsole.Text.Length.CompareTo(inputStartPos) <= 0;

            if (inReadOnlyZone && e.Key == Key.Back)
                e.Handled = true;

            if (inReadOnlyZone)
            {
                if ((e.Key == Key.Left ||
                    e.Key == Key.Right ||
                    e.Key == Key.Up ||
                    e.Key == Key.Down ||
                    (e.Key == Key.C && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))))
                {
                    e.Handled = true;
                }
            }

            if (e.Key == Key.Return)
            {
                string input = richTextBoxConsole.Text.Substring(inputStartPos, (richTextBoxConsole.SelectionStart) - inputStartPos);

                WriteInput(input, Colors.Black, false);
            }
        }


        public void WriteOutput(string output, Color color)
        {
            if (string.IsNullOrEmpty(lastInput) == false &&
                (output == lastInput || output.Replace("\r\n", "") == lastInput))
                return;

            RunOnUIDespatcher(() =>
            {
                richTextBoxConsole.SelectedText += output;
                inputStartPos = richTextBoxConsole.Text.Length;
            });
        }

        public void ClearOutput()
        {
            richTextBoxConsole.Clear();
            inputStartPos = 0;
        }

        public void WriteInput(string input, Color color, bool echo)
        {
            RunOnUIDespatcher(() =>
            {
                if (echo)
                {
                    richTextBoxConsole.SelectedText += input;
                    inputStartPos = richTextBoxConsole.Text.Length;
                }

                lastInput = input;

                processInterace.WriteInput(input);

                FireProcessInputEvent(new ProcessEventArgs(input));
            });
        }

        private void RunOnUIDespatcher(Action action)
        {
            if (Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Dispatcher.BeginInvoke(action, null);
            }
        }

        public void StartProcess(string fileName, string arguments)
        {
            if (ShowDiagnostics)
            {
                WriteOutput("Preparing to run " + fileName, Color.FromArgb(255, 0, 255, 0));
                if (!string.IsNullOrEmpty(arguments))
                    WriteOutput(" with arguments " + arguments + "." + Environment.NewLine, Color.FromArgb(255, 0, 255, 0));
                else
                    WriteOutput("." + Environment.NewLine, Color.FromArgb(255, 0, 255, 0));
            }

            processInterace.StartProcess(fileName, arguments);

            RunOnUIDespatcher(() =>
            {
                if (IsInputEnabled)
                    richTextBoxConsole.IsReadOnly = false;

                IsProcessRunning = true;

            });
        }

        public void StopProcess()
        {
            processInterace.StopProcess();
        }
        private void FireProcessOutputEvent(ProcessEventArgs args)
        {
            var theEvent = OnProcessOutput;
            if (theEvent != null)
                theEvent(this, args);
        }

        private void FireProcessInputEvent(ProcessEventArgs args)
        {
            //  Get the event.
            var theEvent = OnProcessInput;
            if (theEvent != null)
                theEvent(this, args);
        }

        private readonly ProcessInterface processInterace = new ProcessInterface();

        int inputStartPos = -1;

        private string lastInput;


        public event ProcessEventHanlder OnProcessOutput;


        public event ProcessEventHanlder OnProcessInput;

        private static readonly DependencyProperty ShowDiagnosticsProperty =
          DependencyProperty.Register("ShowDiagnostics", typeof(bool), typeof(TerminalControl),
          new PropertyMetadata(false, OnShowDiagnosticsChanged));


        public bool ShowDiagnostics
        {
            get { return (bool)GetValue(ShowDiagnosticsProperty); }
            set { SetValue(ShowDiagnosticsProperty, value); }
        }

        private static void OnShowDiagnosticsChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
        }


        private static readonly DependencyProperty IsInputEnabledProperty =
          DependencyProperty.Register("IsInputEnabled", typeof(bool), typeof(TerminalControl),
          new PropertyMetadata(true));


        public bool IsInputEnabled
        {
            get { return (bool)GetValue(IsInputEnabledProperty); }
            set { SetValue(IsInputEnabledProperty, value); }
        }

        internal static readonly DependencyPropertyKey IsProcessRunningPropertyKey =
          DependencyProperty.RegisterReadOnly("IsProcessRunning", typeof(bool), typeof(TerminalControl),
          new PropertyMetadata(false));

        private static readonly DependencyProperty IsProcessRunningProperty = IsProcessRunningPropertyKey.DependencyProperty;


        public bool IsProcessRunning
        {
            get { return (bool)GetValue(IsProcessRunningProperty); }
            private set { SetValue(IsProcessRunningPropertyKey, value); }
        }

        public ProcessInterface ProcessInterface
        {
            get { return processInterace; }
        }
    }
}
