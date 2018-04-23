using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AutoDog.Models
{
    public class ProjectAlbum : INotifyPropertyChanged
    {
        private int _projectAlbumId;
        private int _projectArtistId;
        private string _title;
        private string _describe;
        private ProjectArtist _projectArtist;

        public int ProjectAlbumId
        {
            get { return _projectAlbumId; }
            set
            {
                if (value == _projectAlbumId) return;
                _projectAlbumId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Artist")]
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

        public string ProjectExtension { get; set; }


        public virtual ProjectArtist ProjectArtist
        {
            get { return _projectArtist; }
            set
            {
                if (Equals(value, _projectArtist)) return;
                _projectArtist = value;
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