using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Factories
{
    public interface IExpensierViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}
