using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AutoDog.Models
{
    public class LocationMap : INotifyPropertyChanged
    {
        private int _locationMapId;
        private string _name;
        private string _title;

        public int LocationMapId
        {
            get { return _locationMapId; }
            set
            {
                if (value == _locationMapId) return;
                _locationMapId = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
