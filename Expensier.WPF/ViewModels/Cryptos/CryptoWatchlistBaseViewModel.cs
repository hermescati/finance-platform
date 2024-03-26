using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Expensier.WPF.ViewModels.Cryptos
{
    public abstract class CryptoWatchlistBaseViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        public AssetViewModel AssetViewModel { get; }


        private readonly ObservableCollection<AssetModel> _assets;
        public IEnumerable<AssetModel> Assets => _assets;


        public CryptoWatchlistBaseViewModel(
            AssetStore assetStore,
            Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filterAssets )
        {
            _assetStore = assetStore;
            _assets = new ObservableCollection<AssetModel>();

            AssetViewModel = new AssetViewModel( _assetStore, filterAssets );

            _assets = (ObservableCollection<AssetModel>) AssetViewModel.Assets;
        }
    }
}
