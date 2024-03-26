using Expensier.WPF.State.Assets;
using System.Linq;


namespace Expensier.WPF.ViewModels.Cryptos
{
    public class TopPerformingCryptosViewModel : CryptoWatchlistBaseViewModel
    {
        public TopPerformingCryptosViewModel( AssetStore assetStore ) : base( assetStore, assets => assets
            .OrderByDescending( c => c.Asset.PercentageChange )
            .Take( 4 ) )
        { }
    }
}