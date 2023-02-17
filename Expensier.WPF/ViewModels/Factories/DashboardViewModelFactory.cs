using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Factories
{
    public class DashboardViewModelFactory : IExpensierViewModelFactory<DashboardViewModel>
    {
        public DashboardViewModel CreateViewModel()
        {
            return new DashboardViewModel();
        }
    }
}
