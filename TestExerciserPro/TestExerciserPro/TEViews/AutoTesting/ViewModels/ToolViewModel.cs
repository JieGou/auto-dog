using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestExerciserPro.TEViews.AutoTesting.ViewModels
{
    class ToolViewModel : PaneViewModel
    {
        public ToolViewModel(string name)
        {
            Name = name;
            Title = name;
        }

        #region Name
        public string Name
        {
            get;
            private set;
        }
        #endregion

        #region IsVisible

        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    RaisePropertyChanged("IsVisible");
                }
            }
        }

        #endregion

    }
}
