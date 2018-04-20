using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AutoDog.Models
{
    public class FileAlbum : INotifyPropertyChanged
    {
        private int _fileAlbumId;
        private int _fileArtistId;
        private string _title;
        private string _describe;
        private FileArtist _fileArtist;

        public int FileAlbumId
        {
            get { return _fileAlbumId; }
            set
            {
                if (value == _fileAlbumId) return;
                _fileAlbumId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("FileArtistId")]
        public int FileArtistId
        {
            get { return _fileArtistId; }
            set
            {
                if (value == _fileArtistId) return;
                _fileArtistId = value;
                OnPropertyChanged();
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value == this.isSelected) return;
                this.isSelected = value;
                this.OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Describe
        {
            get { return _describe; }
            set
            {
                if (value == _describe) return;
                _describe = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Album Description Image")]
        public string DescripImageSource { get; set; }

        [DisplayName("Album Description Image")]
        public string ImageSource { get; set; }

        public string TemplateType{ get; set; }


        public virtual FileArtist FileArtist
        {
            get { return _fileArtist; }
            set
            {
                if (Equals(value, _fileArtist)) return;
                _fileArtist = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}