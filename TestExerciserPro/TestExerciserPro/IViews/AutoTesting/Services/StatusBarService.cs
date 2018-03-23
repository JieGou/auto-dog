using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExerciserPro.IViews.AutoTesting
{
    public interface IStatusBarService
    {
        void ShowMessage(string message);
        void ShowProgress(double currentValue, double allValue);
    }
}
