using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AutoDog.Models;
using AutoDog.UI.Controls.Dialogs;

namespace AutoDog.ViewModels
{
    class NewFileViewModel : INotifyPropertyChanged
    {
        public List<FileAlbum> FileAlbums { get; set; }
        public List<FileArtist> FileArtists { get; set; }

        public NewFileViewModel(IDialogCoordinator dialogCoordinator)
        {
            FileAlbums = SampleData.FileAlbums;
            FileArtists = SampleData.FileArtists;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
