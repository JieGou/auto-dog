using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExerciserPro.IViews.AutoTesting.ViewModel
{
    class TreeViewModel
    {
        public string Icon { get; set; }

        public string EditIcon { get; set; }

        public string DisplayName { get; set; }

        public string Name { get; set; }

        public List<TreeViewModel> Children { get; set; }

        public TreeViewModel()

        {

            Children = new List<TreeViewModel>();

        }
    }
}
