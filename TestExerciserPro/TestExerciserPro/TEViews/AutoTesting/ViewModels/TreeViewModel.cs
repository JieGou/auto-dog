using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TestExerciserPro.TEViews.AutoTesting.ViewModels
{
    class TreeViewModel:SolutionViewModel
    {
        public string Icon { get; set; }

        public string EditIcon { get; set; }

        public string DisplayName { get; set; }

        public string Text { get; set; }

        public bool IsModify { get; set; }

        public bool IsChecked { get; set; }

        public string Tag { get; set; }

        public string IsDeleted { get; set; }

        public string FileType { get; set; }

        public string FolderPath { get; set; }
        public TreeViewItem Child { get; set; }

        public TreeViewModel()
        {
            
        }


        private ObservableCollection<TreeViewModel> _children;
        public ObservableCollection<TreeViewModel> Children
        {
            get { return (_children ?? (_children = new ObservableCollection<TreeViewModel>())); }
        }
    }
}
