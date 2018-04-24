using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;
using AutoDog.UI.Controls.Dialogs;
using AutoDog.UI.Controls;
using AutoDog.Editor.Document;
using AutoDog.ViewModels;
using AutoDog.UI;
using AutoDog.Models;
using NHotkey;
using NHotkey.Wpf;
using Xceed.Wpf.AvalonDock.Layout;
using AutoDog.Commands;

namespace AutoDog
{
    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;

        public ICommand ChangeAccentCommand
        {
            get { return this.changeAccentCommand ?? (changeAccentCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = x => this.DoChangeTheme(x) }); }
        }

        protected virtual void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }
    }

    class Workspace : ViewModelBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        public Workspace(IDialogCoordinator dialogCoordinator)
        {
            this.Title = "AutoDog";
            _dialogCoordinator = dialogCoordinator;
            SampleData.Seed();

            // create accent color menu items for the demo
            this.AccentColors = ThemeManager.Accents
                                            .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                            .ToList();

            // create metro theme color menu items for the demo
            this.AppThemes = ThemeManager.AppThemes
                                           .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();            

            ProjectAlbums = SampleData.ProjectAlbums;
            ProjectArtists = SampleData.ProjectArtists;

            FlipViewImages = new Uri[]
                             {
                                 new Uri("http://www.public-domain-photos.com/free-stock-photos-4/landscapes/mountains/painted-desert.jpg", UriKind.Absolute),
                                 new Uri("http://www.public-domain-photos.com/free-stock-photos-3/landscapes/forest/breaking-the-clouds-on-winter-day.jpg", UriKind.Absolute),
                                 new Uri("http://www.public-domain-photos.com/free-stock-photos-4/travel/bodie/bodie-streets.jpg", UriKind.Absolute)
                             };

            BrushResources = FindBrushResources();

            CultureInfos = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures).OrderBy(c => c.DisplayName).ToList();

            try
            {
                HotkeyManager.Current.AddOrReplace("demo", HotKey.Key, HotKey.ModifierKeys, (sender, e) => OnHotKey(sender, e));
            }
            catch (HotkeyAlreadyRegisteredException exception)
            {
                System.Diagnostics.Trace.TraceWarning("Uups, the hotkey {0} is already registered!", exception.Name);
            }
        }

        public string Title { get; set; }
        public int SelectedIndex { get; set; }
        public List<ProjectAlbum> ProjectAlbums { get; set; }
        public List<ProjectArtist> ProjectArtists { get; set; }
        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }
        public List<CultureInfo> CultureInfos { get; set; }

        int? _integerGreaterProperty;
        public int? IntegerGreaterProperty
        {
            get { return this._integerGreaterProperty; }
            set
            {
                if (Equals(value, _integerGreaterProperty))
                {
                    return;
                }

                _integerGreaterProperty = value;
                RaisePropertyChanged("IntegerGreater10Property");
            }
        }

        DateTime? _datePickerDate;

        [Display(Prompt = "Auto resolved Watermark")]
        public DateTime? DatePickerDate
        {
            get { return this._datePickerDate; }
            set
            {
                if (Equals(value, _datePickerDate))
                {
                    return;
                }

                _datePickerDate = value;
                RaisePropertyChanged("DatePickerDate");
            }
        }

        private bool _quitConfirmationEnabled;
        public bool QuitConfirmationEnabled
        {
            get { return _quitConfirmationEnabled; }
            set
            {
                if (value.Equals(_quitConfirmationEnabled)) return;
                _quitConfirmationEnabled = value;
                RaisePropertyChanged("QuitConfirmationEnabled");
            }
        }

        private bool showMyTitleBar = true;
        public bool ShowMyTitleBar
        {
            get { return showMyTitleBar; }
            set
            {
                if (value.Equals(showMyTitleBar)) return;
                showMyTitleBar = value;
                RaisePropertyChanged("ShowMyTitleBar");
            }
        }

        private bool canCloseFlyout = true;

        public bool CanCloseFlyout
        {
            get { return this.canCloseFlyout; }
            set
            {
                if (Equals(value, this.canCloseFlyout))
                {
                    return;
                }
                this.canCloseFlyout = value;
                this.RaisePropertyChanged("CanCloseFlyout");
            }
        }

        public IEnumerable<string> BrushResources { get; private set; }

        public Uri[] FlipViewImages
        {
            get;
            set;
        }

        private IEnumerable<string> FindBrushResources()
        {
            var rd = new ResourceDictionary
            {
                Source = new Uri(@"/AutoDog.UI;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute)
            };

            var resources = rd.Keys.Cast<object>()
                    .Where(key => rd[key] is SolidColorBrush)
                    .Select(key => key.ToString())
                    .OrderBy(s => s)
                    .ToList();

            return resources;
        }

        public class RandomDataTemplateSelector : DataTemplateSelector
        {
            public DataTemplate TemplateOne { get; set; }

            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                return TemplateOne;
            }
        }

        private HotKey _hotKey = new HotKey(Key.Home, ModifierKeys.Control | ModifierKeys.Shift);

        public HotKey HotKey
        {
            get { return _hotKey; }
            set
            {
                if (_hotKey != value)
                {
                    _hotKey = value;
                    if (_hotKey != null && _hotKey.Key != Key.None)
                    {
                        HotkeyManager.Current.AddOrReplace("demo", HotKey.Key, HotKey.ModifierKeys, (sender, e) => OnHotKey(sender, e));
                    }
                    else
                    {
                        HotkeyManager.Current.Remove("demo");
                    }
                    RaisePropertyChanged("HotKey");
                }
            }
        }

        private async Task OnHotKey(object sender, HotkeyEventArgs e)
        {
            await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync(
                "Hotkey pressed",
                "You pressed the hotkey '" + HotKey + "' registered with the name '" + e.Name + "'");
        }

        private ICommand toggleIconScalingCommand;

        public ICommand ToggleIconScalingCommand
        {
            get
            {
                return toggleIconScalingCommand ?? (toggleIconScalingCommand = new SimpleCommand
                {
                    ExecuteDelegate = ToggleIconScaling
                });
            }
        }

        private void ToggleIconScaling(object obj)
        {
            var multiFrameImageMode = (MultiFrameImageMode)obj;
            ((MetroWindow)Application.Current.MainWindow).IconScalingMode = multiFrameImageMode;
            RaisePropertyChanged("IsScaleDownLargerFrame");
            RaisePropertyChanged("IsNoScaleSmallerFrame");
        }

        public bool IsScaleDownLargerFrame { get { return ((MetroWindow)Application.Current.MainWindow).IconScalingMode == MultiFrameImageMode.ScaleDownLargerFrame; } }

        public bool IsNoScaleSmallerFrame { get { return ((MetroWindow)Application.Current.MainWindow).IconScalingMode == MultiFrameImageMode.NoScaleSmallerFrame; } }

        static Workspace _this = new Workspace(DialogCoordinator.Instance);

        public static Workspace This
        {
            get { return _this; }
        }


        ObservableCollection<FileViewModel> _files = new ObservableCollection<FileViewModel>();
        ReadOnlyObservableCollection<FileViewModel> _readonyFiles = null;
        public ReadOnlyObservableCollection<FileViewModel> Files
        {
            get
            {
                if (_readonyFiles == null)
                    _readonyFiles = new ReadOnlyObservableCollection<FileViewModel>(_files);

                return _readonyFiles;
            }
        }

        ToolViewModel[] _tools = null;

        public IEnumerable<ToolViewModel> Tools
        {
            get
            {
                if (_tools == null)
                    _tools = new ToolViewModel[] { FileStats,SolutionView, ResourcesView, ClassView, ErrorListView, OutPutView};
                return _tools;
            }
        }

        FileStatsViewModel _fileStats = null;
        public FileStatsViewModel FileStats
        {
            get
            {
                if (_fileStats == null)
                    _fileStats = new FileStatsViewModel();

                return _fileStats;
            }
        }

        SolutionViewModel _solutionView = null;
        public SolutionViewModel SolutionView
        {
            get
            {
                if (_solutionView == null)
                    _solutionView = new SolutionViewModel();
                return _solutionView;
            }
        }

        ClassViewModel _classView = null;
        public ClassViewModel ClassView
        {
            get
            {
                if (_classView == null)
                    _classView = new ClassViewModel();
                return _classView;
            }
        }

        ErrorListViewModel _errorListView = null;
        public ErrorListViewModel ErrorListView
        {
            get
            {
                if (_errorListView == null)
                    _errorListView = new ErrorListViewModel();
                return _errorListView;
            }
        }

        OutPutViewModel _outPutView = null;
        public OutPutViewModel OutPutView
        {
            get
            {
                if (_outPutView == null)
                    _outPutView = new OutPutViewModel();
                return _outPutView;
            }
        }

        ResourcesViewModel _resourcesView;
        public ResourcesViewModel ResourcesView
        {
            get
            {
                if (_resourcesView == null)
                    _resourcesView = new ResourcesViewModel();
                return _resourcesView;
            }
        }

        #region OpenFile
        RelayCommand _openFile = null;
        public ICommand OpenFile
        {
            get
            {
                if (_openFile == null)
                {
                    _openFile = new RelayCommand((p) => OnOpen(p), (p) => CanOpen(p));
                }

                return _openFile;
            }
        }

        private bool CanOpen(object parameter)
        {
            return true;
        }

        private void OnOpen(object parameter)
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "打开文件";
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                var fileViewModel = Open(dlg.FileName);
                ActiveDocument = fileViewModel;
            }
        }

        public FileViewModel Open(string filepath)
        {
            var fileViewModel = _files.FirstOrDefault(fm => fm.FilePath == filepath);
            if (fileViewModel != null)
                return fileViewModel;

            fileViewModel = new FileViewModel(filepath);
            _files.Add(fileViewModel);
            return fileViewModel;
        }

        #endregion 

        #region NewFile
        RelayCommand _newFile = null;
        public ICommand NewFile
        {
            get
            {
                if (_newFile == null)
                {
                    _newFile = new RelayCommand((p) => OnNew(p), (p) => CanNew(p));
                }

                return _newFile;
            }
        }

        private bool CanNew(object parameter)
        {
            return true;
        }

        private void OnNew(object parameter)
        {
            _files.Add(new FileViewModel(){ Document = new TextDocument() });
            ActiveDocument = _files.Last();
        }

        #endregion 

        #region ActiveDocument

        private FileViewModel _activeDocument = null;
        public FileViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    RaisePropertyChanged("ActiveDocument");
                    if (ActiveDocumentChanged != null)
                        ActiveDocumentChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ActiveDocumentChanged;

        #endregion

        internal void Close(FileViewModel fileToClose)
        {
            ShowFileClosingDialog(fileToClose);
        }

        internal void Save(FileViewModel fileToSave, bool saveAsFlag = false)
        {
            if (fileToSave.FilePath == null || saveAsFlag)
            {
                var dlg = new SaveFileDialog();

                //设置对话框标题
                dlg.Title = "保存文件";

                //设置文件类型
                dlg.Filter = "txt files(*.txt)|*.txt|xls files(*.xls)|*.xls|All files(*.*)|*.*";

                //设置默认文件名（可以不设置）
                dlg.FileName = "Document1";

                //主设置默认文件extension（可以不设置）
                dlg.DefaultExt = "txt";

                //获取或设置一个值，该值指示如果用户省略扩展名，文件对话框是否自动在文件名中添加扩展名。（可以不设置）
                dlg.AddExtension = true;

                //设置默认文件类型显示顺序（可以不设置）
                dlg.FilterIndex = 2;

                //保存对话框是否记忆上次打开的目录
                dlg.RestoreDirectory = true;

                bool? result = dlg.ShowDialog();
                if (result==true)
                {
                    fileToSave.FilePath = dlg.FileName.ToString();
                    SaveFileDirectly(fileToSave);
                }
            }
            else { SaveFileDirectly(fileToSave); }
        }

        void SaveFileDirectly(FileViewModel fileToSave)
        {
            File.WriteAllText(fileToSave.FilePath, fileToSave.Document.Text);
            ActiveDocument.IsDirty = false;
        }

        public async void ShowFileClosingDialog(FileViewModel fileToClose)
        {
            MetroWindow window = MainWindow.metroWindow;
            if (fileToClose.IsDirty)
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "保存",
                    NegativeButtonText = "不保存",
                    FirstAuxiliaryButtonText = "关闭",
                    ColorScheme = window.MetroDialogOptions.ColorScheme
                };

                MessageDialogResult result = await window.ShowMessageAsync("提示信息", "确定要保存当前文档吗？",
                    MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);


                if (result == MessageDialogResult.FirstAuxiliary) return;
                if (result == MessageDialogResult.Affirmative) Save(fileToClose);
                if (result == MessageDialogResult.Negative) _files.Remove(fileToClose);
            }
            else { _files.Remove(fileToClose); } 
        }
    }
}
