using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AutoDog.UI.Controls.Dialogs;
using AutoDog.Models;

namespace AutoDog.ViewModels
{
    class NewProjectViewModel : INotifyPropertyChanged
    {
        public List<Album> Albums { get; set; }
        public List<Artist> Artists { get; set; }
        public NewProjectViewModel(IDialogCoordinator dialogCoordinator)
        {
            SampleData.Seed();
            Albums = SampleData.Albums;
            Artists = SampleData.Artists;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
