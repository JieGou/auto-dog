using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AutoDog.Models
{
    public class Album : INotifyPropertyChanged
    {
        private int _albumId;
        private int _artistId;
        private string _title;
        private string _describe;
        private Artist _artist;

        public int AlbumId
        {
            get { return _albumId; }
            set
            {
                if (value == _albumId) return;
                _albumId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Artist")]
        public int ArtistId
        {
            get { return _artistId; }
            set
            {
                if (value == _artistId) return;
                _artistId = value;
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
        public string DescripImage { get; set; }

        [DisplayName("Album Description Image")]
        public string ImagePath { get; set; }


        public virtual Artist Artist
        {
            get { return _artist; }
            set
            {
                if (Equals(value, _artist)) return;
                _artist = value;
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