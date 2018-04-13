using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TestExerciserPro.TEViews.AutoTesting.ViewModels
{
    public partial class TreeViewModel:ViewModelBase
    {
        public string IconSelected { get; set; }
        public string IconClosed { get; set; }
        public string IconOpened { get; set; }
        public Enum ItemType { get; set; }

        bool isInEditMode = false;
        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                isInEditMode = value;
                RaisePropertyChanged("IsInEditMode");
            }
        }

        public List<TreeViewModel> Children { get; set; }

        public TreeViewModel()
        {
            Children = new List<TreeViewModel>();
        }
    }
}
