using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Assets;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Assets;
using Expensier.WPF.ViewModels.Charts;
using Expensier.WPF.ViewModels.Modals;


namespace Expensier.WPF.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        public AssetViewModel AssetViewModel { get; }
        public AssetWatchlistViewModel AssetWatchlistViewModel { get; }
        public PortfolioValueViewModel PortfolioValueViewModel { get; }
        public PortfolioPerformanceViewModel PortfolioPerformanceViewModel { get; }
        public AssetAllocationViewModel AssetAllocationViewModel { get; }
        public CryptoModalViewModel CryptoModalViewModel { get; }


        public WalletViewModel(
            AssetWatchlistViewModel assetWatchlistViewModel,
            PortfolioValueViewModel portfolioValueViewModel,
            AssetAllocationViewModel assetAllocationViewModel,
            PortfolioPerformanceViewModel portfolioPerformanceViewModel,
            AssetStore assetStore,
            CryptoModalViewModel cryptoModalViewModel,
            IAssetService assetService,
            AccountStore accountStore,
            IRenavigator renavigator )
        {
            AssetWatchlistViewModel = assetWatchlistViewModel;
            PortfolioValueViewModel = portfolioValueViewModel;
            AssetAllocationViewModel = assetAllocationViewModel;
            PortfolioPerformanceViewModel = portfolioPerformanceViewModel;
            AssetViewModel = new AssetViewModel( accountStore, assetStore, assetService, renavigator );
            CryptoModalViewModel = cryptoModalViewModel;
        }
    }
}
