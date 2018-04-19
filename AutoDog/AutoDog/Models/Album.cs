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
        private int _genreId;
        private int _artistId;
        private string _title;
        private decimal _price;
        private Genre _genre;
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

        [DisplayName("Genre")]
        public int GenreId
        {
            get { return _genreId; }
            set
            {
                if (value == _genreId) return;
                _genreId = value;
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

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value == _price) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Album Art URL")]
        public string AlbumArtUrl { get; set; }

        public virtual Genre Genre
        {
            get { return _genre; }
            set
            {
                if (Equals(value, _genre)) return;
                _genre = value;
                OnPropertyChanged();
            }
        }

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