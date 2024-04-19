using Expensier.Domain.Services.Portfolio;
using Expensier.WPF.State.Accounts;


namespace Expensier.WPF.ViewModels.Assets
{
    public class PortfolioValueViewModel : ViewModelBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly AccountStore _accountStore;


        private double _totalValue;
        public double TotalValue
        {
            get => _totalValue;
            set
            {
                _totalValue = value;
                OnPropertyChanged( nameof( TotalValue ) );
            }
        }


        public PortfolioValueViewModel( IPortfolioService portfolioService, AccountStore accountStore )
        {
            _portfolioService = portfolioService;
            _accountStore = accountStore;

            TotalValue = _portfolioService.GetPortfolioValue( _accountStore.CurrentAccount );
        }
    }
}
