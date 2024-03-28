using Expensier.Domain.Services;
using Expensier.WPF.State.Assets;
using System.Linq;


namespace Expensier.WPF.ViewModels.Assets
{
    public class AssetWatchlistViewModel : AssetWatchlistBaseViewModel
    {
        public AssetWatchlistViewModel( AssetStore assetStore, IAssetService assetService ) : base( 
            assetStore, 
            assetService,
            assets => assets
                .OrderByDescending( c => c.Asset.PercentageChange ) )
        { }
    }
}
