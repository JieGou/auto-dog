using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AutoDog.Models
{
    public class FileArtist : INotifyPropertyChanged
    {
        private int _fileArtistId;
        private string _name;
        private List<FileAlbum> _fileAlbums;

        public int fileArtistId
        {
            get { return _fileArtistId; }
            set
            {
                if (value == _fileArtistId) return;
                _fileArtistId = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public List<FileAlbum> FileAlbums
        {
            get { return _fileAlbums; }
            set
            {
                if (Equals(value, _fileAlbums)) return;
                _fileAlbums = value;
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