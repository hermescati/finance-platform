using Expensier.Domain.Services;
using Expensier.WPF.State.Assets;
using System.Linq;


namespace Expensier.WPF.ViewModels.Cryptos
{
    public class CryptoWatchlistViewModel : CryptoWatchlistBaseViewModel
    {
        public CryptoWatchlistViewModel( AssetStore assetStore, IAssetService assetService ) : base( 
            assetStore, 
            assetService,
            assets => assets
                .OrderByDescending( c => c.Asset.PercentageChange ) )
        { }
    }
}
