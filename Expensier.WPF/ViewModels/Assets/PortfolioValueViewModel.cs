using Expensier.Domain.Models;
using Expensier.Domain.Services.Portfolio;
using Expensier.WPF.State.Accounts;
using System.Collections.Generic;
using System.Linq;


namespace Expensier.WPF.ViewModels.Assets
{
    public class PortfolioValueViewModel : ViewModelBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly AccountStore _accountStore;
        private IEnumerable<AssetTransaction> _assets;


        private double _cryptosValue;
        public double CryptosValue
        {
            get => _cryptosValue;
            set
            {
                _cryptosValue = value;
                OnPropertyChanged( nameof( CryptosValue ) );
            }
        }

        private double _cryptosInvestment;
        public double CryptosInvestment
        {
            get => _cryptosInvestment;
            set
            {
                _cryptosInvestment = value;
                OnPropertyChanged( nameof( CryptosInvestment ) );
            }
        }

        private double _cryptosROI;
        public double CryptosROI
        {
            get => _cryptosROI;
            set
            {
                _cryptosROI = value;
                OnPropertyChanged( nameof( CryptosROI ) );
            }
        }


        public PortfolioValueViewModel( IPortfolioService portfolioService, AccountStore accountStore )
        {
            _portfolioService = portfolioService;
            _accountStore = accountStore;
            _assets = _accountStore.CurrentAccount.AssetList;

            IEnumerable<AssetTransaction> cryptos = _assets.Where( a => a.Category == AssetTransaction.AssetType.Cryptocurrency );
            
            double portfolioValue = _portfolioService.FindTotalValue( _assets );

            CryptosValue = _portfolioService.FindTotalValue( cryptos );
            CryptosInvestment = _portfolioService.FindInitialInvestment( cryptos );
            CryptosROI = CryptosValue / CryptosInvestment - 1;
        }
    }
}
