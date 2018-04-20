using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AutoDog.Models
{
    public class ProjectArtist : INotifyPropertyChanged
    {
        private int _projectArtistId;
        private string _name;
        private List<ProjectAlbum> _projectAlbums;

        public int ProjectArtistId
        {
            get { return _projectArtistId; }
            set
            {
                if (value == _projectArtistId) return;
                _projectArtistId = value;
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

        public List<ProjectAlbum> ProjectAlbums
        {
            get { return _projectAlbums; }
            set
            {
                if (Equals(value, _projectAlbums)) return;
                _projectAlbums = value;
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