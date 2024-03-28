using Expensier.Domain.Services;
using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Expensier.WPF.ViewModels.Assets
{
    public abstract class AssetWatchlistBaseViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly IAssetService _assetService;
        public AssetViewModel AssetViewModel { get; }


        private readonly ObservableCollection<AssetModel> _assets;
        public IEnumerable<AssetModel> Assets => _assets;


        public AssetWatchlistBaseViewModel(
            AssetStore assetStore,
            IAssetService assetService,
            Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filterAssets )
        {
            _assetStore = assetStore;
            _assetService = assetService;
            _assets = new ObservableCollection<AssetModel>();

            AssetViewModel = new AssetViewModel( _assetStore, _assetService, filterAssets );

            _assets = (ObservableCollection<AssetModel>) AssetViewModel.Assets;
        }
    }
}
