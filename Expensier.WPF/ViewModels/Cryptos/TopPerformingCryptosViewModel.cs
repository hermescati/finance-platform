using Expensier.Domain.Services;
using Expensier.WPF.State.Assets;
using System.Linq;


namespace Expensier.WPF.ViewModels.Cryptos
{
    public class TopPerformingCryptosViewModel : CryptoWatchlistBaseViewModel
    {
        public TopPerformingCryptosViewModel( AssetStore assetStore, IAssetService assetService ) : base( 
            assetStore, 
            assetService,
            assets => assets
                .OrderByDescending( c => c.Asset.PercentageChange )
                .Take( 4 ) )
        { }
    }
}