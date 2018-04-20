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
    class NewProjectViewModel : INotifyPropertyChanged
    {
        public List<ProjectAlbum> ProjectAlbums { get; set; }
        public List<ProjectArtist> ProjectArtists { get; set; }

        public NewProjectViewModel(IDialogCoordinator dialogCoordinator)
        {
            ProjectAlbums = SampleData.ProjectAlbums;
            ProjectArtists = SampleData.ProjectArtists;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
