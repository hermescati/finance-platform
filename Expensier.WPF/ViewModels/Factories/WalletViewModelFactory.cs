using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Factories
{
    public class WalletViewModelFactory : IExpensierViewModelFactory<WalletViewModel>
    {
        public WalletViewModel CreateViewModel()
        {
            return new WalletViewModel();
        }
    }
}
