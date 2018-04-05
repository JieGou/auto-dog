using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using TestExerciserPro.UI.Controls.Dialogs;
using TestExerciserPro.IViews.AutoTesting.Models;

namespace TestExerciserPro.IViews.AutoTesting.ViewModels
{
    class NewProjectViewModel : INotifyPropertyChanged
    {
        public List<Album> Albums { get; set; }
        public List<Artist> Artists { get; set; }

        public List<SolutionType> SolutionType { get; set; }
        public NewProjectViewModel(IDialogCoordinator dialogCoordinator)
        {
            SampleData.Seed();
            Albums = SampleData.Albums;
            Artists = SampleData.Artists;
            SolutionType = SampleData.SolutionTypes;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
