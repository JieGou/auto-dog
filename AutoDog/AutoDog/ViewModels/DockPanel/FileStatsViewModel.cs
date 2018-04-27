
using System;
using System.IO;
using AutoDog.Logics;

namespace AutoDog.ViewModels
{
    class FileStatsViewModel : ToolViewModel
    {
        public FileStatsViewModel()
            :base("文件属性")
        {
            Workspace.This.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);
            ContentId = ToolContentId;
            IconSource = Common.ConvertImageToBitMap("pack://application:,,/Images/DockPanel/PropertyView.png");
        }

        public const string ToolContentId = "FileStatsTool";

        void OnActiveDocumentChanged(object sender, EventArgs e)
        {
            if (Workspace.This.ActiveDocument != null &&
                Workspace.This.ActiveDocument.FilePath != null &&
                File.Exists(Workspace.This.ActiveDocument.FilePath))
            {
                var fi = new FileInfo(Workspace.This.ActiveDocument.FilePath);
                FileSize = fi.Length;
                LastModified = fi.LastWriteTime;
            }
            else
            {
                FileSize = 0;
                LastModified = DateTime.MinValue;
            }
        }

        #region FileSize

        private long _fileSize;
        public long FileSize
        {
            get { return _fileSize; }
            set
            {
                if (_fileSize != value)
                {
                    _fileSize = value;
                    RaisePropertyChanged("FileSize");
                }
            }
        }

        #endregion

        #region LastModified

        private DateTime _lastModified;
        public DateTime LastModified
        {
            get { return _lastModified; }
            set
            {
                if (_lastModified != value)
                {
                    _lastModified = value;
                    RaisePropertyChanged("LastModified");
                }
            }
        }

        #endregion

    }
}
