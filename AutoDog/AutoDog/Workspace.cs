using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
//using System.Windows.Forms;
using System.IO;
using AutoDog.UI.Controls.Dialogs;
using AutoDog.UI.Controls;
using AutoDog.Editor.Document;
using AutoDog.ViewModels;

namespace AutoDog
{
    class Workspace : ViewModelBase
    {
        protected Workspace()
        { 

        }

        static Workspace _this = new Workspace();

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
                    _tools = new ToolViewModel[] { FileStats,SolutionView,Resources,ClassView, ErrorList,OutPut};
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

        ErrorListViewModel _errorList = null;
        public ErrorListViewModel ErrorList
        {
            get
            {
                if (_errorList == null)
                    _errorList = new ErrorListViewModel();
                return _errorList;
            }
        }

        OutPutViewModel _outPut = null;
        public OutPutViewModel OutPut
        {
            get
            {
                if (_outPut == null)
                    _outPut = new OutPutViewModel();
                return _outPut;
            }
        }

        ResourcesViewModel _resources;
        public ResourcesViewModel Resources
        {
            get
            {
                if (_resources == null)
                    _resources = new ResourcesViewModel();
                return _resources;
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
            MetroWindow window = MainAutoTesting.AutoTestingWindow;
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
