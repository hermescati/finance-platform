using Expensier.Domain.Services.Portfolio;
using Expensier.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Cryptos
{
    public class PortfolioValueViewModel : ViewModelBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly AccountStore _accountStore; 

        private double _totalValue;
        public double TotalValue
        {
            get
            {
                return _totalValue;
            }
            set
            {
                _totalValue = value;
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public PortfolioValueViewModel(IPortfolioService portfolioService, AccountStore accountStore)
        {
            _portfolioService = portfolioService; 
            _accountStore = accountStore;

            TotalValue = _portfolioService.GetMarketValue(_accountStore.CurrentAccount);
        }
    }
}
