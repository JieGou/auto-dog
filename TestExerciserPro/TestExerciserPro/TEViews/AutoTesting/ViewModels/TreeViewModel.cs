using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TestExerciserPro.TEViews.AutoTesting.ViewModels
{
    class TreeViewModel:ViewModelBase
    {
        public string Icon { get; set; }
        public string EditIcon { get; set; }
        public string Header { get; set; }
        public string Tag { get; set; }
        public bool IsExpand { get; set; }
        public Enum ItemType { get; set; }

        protected bool isChecked;
        /// <summary>
        /// 是否被勾选
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }
        public List<TreeViewModel> Children { get; set; }


        public TreeViewModel()
        {
            Children = new List<TreeViewModel>();
        }
    }
}
