/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
//using System.Windows.Forms;
using System.IO;
using TestExerciserPro.UI.Controls.Dialogs;
using TestExerciserPro.UI.Controls;
using TestExerciserPro.Editor.Document;
using TestExerciserPro.TEViews.AutoTesting.ViewModels;

namespace TestExerciserPro.TEViews.AutoTesting
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
                    _tools = new ToolViewModel[] { FileStats,SolutionView, Resources,ClassView, ErrorList,OutPut};
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
            if (fileToClose.IsDirty)
            {
                ShowMessageDialog(MainAutoTesting.AutoTestingWindow, fileToClose);
            }
        }

        internal void Save(FileViewModel fileToSave, bool saveAsFlag = false)
        {
            if (fileToSave.FilePath == null || saveAsFlag)
            {
                var dlg = new SaveFileDialog();
                if (dlg.ShowDialog().GetValueOrDefault())
                {
                    fileToSave.FilePath = dlg.SafeFileName;
                    File.WriteAllText(fileToSave.FilePath, fileToSave.Document.Text);
                    ActiveDocument.IsDirty = false;
                }
            }
        }

        public async void ShowMessageDialog(MetroWindow window,FileViewModel fileToClose)
        {
            // This demo runs on .Net 4.0, but we're using the Microsoft.Bcl.Async package so we have async/await support
            // The package is only used by the demo and not a dependency of the library!
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "保存",
                NegativeButtonText = "不保存",
                FirstAuxiliaryButtonText = "关闭",
                ColorScheme = window.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await window.ShowMessageAsync("提示信息", "确定要保存当前文档吗？",
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);


            if (result == MessageDialogResult.FirstAuxiliary)
                return;
            else if(result == MessageDialogResult.Affirmative)
                Save(fileToClose);
            else if(result == MessageDialogResult.Negative)
                _files.Remove(fileToClose);
        }

    }
}
