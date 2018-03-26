using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using TestExerciserPro.Editor;
using TestExerciserPro.Editor.CodeCompletion;
using TestExerciserPro.Editor.Folding;
using TestExerciserPro.Editor.Highlighting;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock.Layout;
using System.ComponentModel;
using TestExerciserPro.UI.Controls.Dialogs;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace TestExerciserPro.IViews.AutoTesting
{
    /// <summary>
    /// MainAutoTesting.xaml 的交互逻辑
    /// </summary>
    public partial class MainAutoTesting
    {

        #region 变量
        int clickCount = 0;
        string currentFileName;
        static TextEditor currentTextEditor;
        FoldingManager foldingManager;
        object foldingStrategy;
        CompletionWindow completionWindow;
        private bool closeMe;

        #endregion
        public MainAutoTesting()
        {
            InitializeComponent();

            this.DataContext = Workspace.This;

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);

        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.LayoutSerializationCallback += (s, args) =>
            {
                args.Content = args.Content;
            };

            if (File.Exists(@".\AvalonDock.config"))
                serializer.Deserialize(@".\AvalonDock.config");
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(@".\AvalonDock.config");
        }

        #region LoadLayoutCommand
        RelayCommand _loadLayoutCommand = null;
        public ICommand LoadLayoutCommand
        {
            get
            {
                if (_loadLayoutCommand == null)
                {
                    _loadLayoutCommand = new RelayCommand((p) => OnLoadLayout(p), (p) => CanLoadLayout(p));
                }

                return _loadLayoutCommand;
            }
        }

        private bool CanLoadLayout(object parameter)
        {
            return File.Exists(@".\AvalonDock.Layout.config");
        }

        private void OnLoadLayout(object parameter)
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            //Here I've implemented the LayoutSerializationCallback just to show
            // a way to feed layout desarialization with content loaded at runtime
            //Actually I could in this case let AvalonDock to attach the contents
            //from current layout using the content ids
            //LayoutSerializationCallback should anyway be handled to attach contents
            //not currently loaded
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
            {
                //if (e.Model.ContentId == FileStatsViewModel.ToolContentId)
                //    e.Content = Workspace.This.FileStats;
                //else if (!string.IsNullOrWhiteSpace(e.Model.ContentId) &&
                //    File.Exists(e.Model.ContentId))
                //    e.Content = Workspace.This.Open(e.Model.ContentId);
            };
            layoutSerializer.Deserialize(@".\AvalonDock.Layout.config");
        }

        #endregion 

        #region SaveLayoutCommand
        RelayCommand _saveLayoutCommand = null;
        public ICommand SaveLayoutCommand
        {
            get
            {
                if (_saveLayoutCommand == null)
                {
                    _saveLayoutCommand = new RelayCommand((p) => OnSaveLayout(p), (p) => CanSaveLayout(p));
                }

                return _saveLayoutCommand;
            }
        }

        private bool CanSaveLayout(object parameter)
        {
            return true;
        }

        private void OnSaveLayout(object parameter)
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(@".\AvalonDock.Layout.config");
        }

        #endregion 

        private void OnDumpToConsole(object sender, RoutedEventArgs e)
        {
            // Uncomment when TRACE is activated on AvalonDock project
            dockManager.Layout.ConsoleDump(0);
        }


        private void HightLightingDocument()
        {
            // Load our custom highlighting definition
            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(MainAutoTesting).Assembly.GetManifestResourceStream("TestExerciserPro.IViews.AutoTesting.CustomHighlighting.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not find embedded resource");
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = TestExerciserPro.Editor.Highlighting.Xshd.
                        HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            // and register it in the HighlightingManager
            HighlightingManager.Instance.RegisterHighlighting("Custom Highlighting", new string[] { ".cool" }, customHighlighting);

        }


        private void openFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog() ?? false)
            {
                currentFileName = dlg.FileName;
                addDocumentItems(sender,e);
            }
        }

        public void addDocumentItems(object sender, RoutedEventArgs e)
        {
            //clickCount++;
            //LayoutAnchorable layOutAnc = new LayoutAnchorable() { Title = Path.GetFileName(currentFileName),CanClose = true};
            //layOutAnc.Closing += layOutAnc_DocumentClosing;
            //TextEditor myEditor = new TextEditor() { SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#")};
            //myEditor.Load(currentFileName);
            //myEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(currentFileName));
            //myEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            //myEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
            //SearchPanel.Install(myEditor);
            //DispatcherTimer foldingUpdateTimer = new DispatcherTimer();
            //foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
            //foldingUpdateTimer.Tick += delegate { UpdateFoldings(myEditor); };
            //foldingUpdateTimer.Start();
            //layOutAnc.Content = myEditor;
            //layOutPane.Children.Add(layOutAnc);           
            //layOutAnc.IsActive = true;
            //currentTextEditor = myEditor;
            //highlightingComboBox.Text = currentTextEditor.SyntaxHighlighting.Name;
        }

        private void saveFileClick(object sender, EventArgs e)
        {
            if (currentFileName == null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".txt";
                if (dlg.ShowDialog() ?? false)
                {
                    currentFileName = dlg.FileName;
                }
                else
                {
                    return;
                }
            }
            currentTextEditor.Save(currentFileName);
        }

        private void propertyGridComboBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            //if (propertyGrid == null)
            //    return;
            //switch (propertyGridComboBox.SelectedIndex)
            //{
            //    case 0:
            //        propertyGrid.SelectedObject = textEditor;
            //        break;
            //    case 1:
            //        propertyGrid.SelectedObject = textEditor.TextArea;
            //        break;
            //    case 2:
            //        propertyGrid.SelectedObject = textEditor.Options;
            //        break;
            //}
        }

        private void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            //if (e.Text == ".")
            //{
            //    // open code completion after the user has pressed dot:
            //    completionWindow = new CompletionWindow(currentTextEditor.TextArea);
            //    // provide AvalonEdit with the data:
            //    IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
            //    data.Add(new MyCompletionData("Item1"));
            //    data.Add(new MyCompletionData("Item2"));
            //    data.Add(new MyCompletionData("Item3"));
            //    data.Add(new MyCompletionData("Another item"));
            //    completionWindow.Show();
            //    completionWindow.Closed += delegate
            //    {
            //        completionWindow = null;
            //    };
            //}
        }

        private void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // do not set e.Handled=true - we still want to insert the character that was typed
        }

        #region Folding
       
        private void HighlightingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (currentTextEditor != null)
            //{
            //    if (currentTextEditor.SyntaxHighlighting == null)
            //    {
            //        foldingStrategy = null;
            //    }
            //    else
            //    {
            //        switch (currentTextEditor.SyntaxHighlighting.Name)
            //        {
            //            case "XML":
            //                foldingStrategy = new XmlFoldingStrategy();
            //                currentTextEditor.TextArea.IndentationStrategy = new TestExerciserPro.Editor.Indentation.DefaultIndentationStrategy();
            //                break;
            //            case "C#":
            //            case "C++":
            //            case "PHP":
            //            case "Java":
            //                currentTextEditor.TextArea.IndentationStrategy = new TestExerciserPro.Editor.Indentation.CSharp.CSharpIndentationStrategy(currentTextEditor.Options);
            //                foldingStrategy = new BraceFoldingStrategy();
            //                break;
            //            default:
            //                currentTextEditor.TextArea.IndentationStrategy = new TestExerciserPro.Editor.Indentation.DefaultIndentationStrategy();
            //                foldingStrategy = null;
            //                break;
            //        }
            //    }
            //    if (foldingStrategy != null)
            //    {
            //        if (foldingManager == null)
            //            foldingManager = FoldingManager.Install(currentTextEditor.TextArea);
            //        UpdateFoldings(currentTextEditor);
            //    }
            //    else
            //    {
            //        if (foldingManager != null)
            //        {
            //            FoldingManager.Uninstall(foldingManager);
            //            foldingManager = null;
            //        }
            //    }
            //}          
        }

        //private void UpdateFoldings(TextEditor textEditor)
        //{
        //    if (foldingStrategy is BraceFoldingStrategy)
        //    {
        //        ((BraceFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, textEditor.Document);
        //    }
        //    if (foldingStrategy is XmlFoldingStrategy)
        //    {
        //        ((XmlFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, textEditor.Document);
        //    }
        //}
        #endregion

        private async void layOutAnc_DocumentClosing(object sender, CancelEventArgs e)
        {
            var currentLayOutAnc = sender as LayoutAnchorable;
            if (e.Cancel) return;

            // we want manage the closing itself!
            e.Cancel = !this.closeMe;
            // yes we want now really close the window
            if (this.closeMe) return;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "退出",
                NegativeButtonText = "取消",
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(
                "关闭当前文档？",
                "确定要关闭当前文档吗？关闭后，未保存内容将会丢失！",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

                this.closeMe = result == MessageDialogResult.Affirmative;

            if (this.closeMe) currentLayOutAnc.Close();
        }

        private async void MainAutoTesting_WinddowClosing(object sender, CancelEventArgs e)
        {
            if (e.Cancel) return;

            // we want manage the closing itself!
            e.Cancel = !this.closeMe;
            // yes we want now really close the window
            if (this.closeMe) return;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "退出",
                NegativeButtonText = "取消",
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(
                "退出窗口?",
                "确定要退出当前窗口吗?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            this.closeMe = result == MessageDialogResult.Affirmative;

            if (this.closeMe) this.Close();
        }
    }
}

