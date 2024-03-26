using Expensier.WPF.State.Assets;
using System.Linq;


namespace Expensier.WPF.ViewModels.Cryptos
{
    public class CryptoWatchlistViewModel : CryptoWatchlistBaseViewModel
    {
        public CryptoWatchlistViewModel( AssetStore assetStore ) : base( assetStore, assets => assets
                .OrderByDescending( c => c.Asset.PercentageChange ) )
        { }
    }
}
