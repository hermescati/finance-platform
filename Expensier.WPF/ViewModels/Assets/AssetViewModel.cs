using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Assets;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace Expensier.WPF.ViewModels.Assets
{
    public class AssetViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly AssetStore _assetStore;
        private readonly IAssetService _assetService;
        private readonly IRenavigator _renavigator;
        private readonly Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> _filteredAssets;


        private readonly ObservableCollection<AssetModel> _assets;
        public IEnumerable<AssetModel> Assets => _assets;


        private bool _listEmpty;
        public bool ListEmpty
        {
            get => _listEmpty;
            set
            {
                _listEmpty = value;
                OnPropertyChanged( nameof( ListEmpty ) );
            }
        }

        private SortOptions _selectedOption;
        public SortOptions SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged( nameof( SelectedOption ) );
                SortAssets( SelectedOption );
            }
        }
        public IEnumerable<SortOptions> SortItems { get; } = SortOptions.AssetOptions();


        public AssetViewModel(
            AccountStore accountStore,
            AssetStore assetStore,
            IAssetService assetService,
            IRenavigator renavigator ) : this(
                accountStore,
                assetStore,
                assetService,
                renavigator,
                assets => assets )
        { }


        public AssetViewModel(
            AccountStore accountStore,
            AssetStore assetStore,
            IAssetService assetService,
            IRenavigator renavigator,
            Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filteredAssets )
        {
            _accountStore = accountStore;
            _assetStore = assetStore;
            _assetService = assetService;
            _renavigator = renavigator;
            _filteredAssets = filteredAssets;

            _assets = new ObservableCollection<AssetModel>();
            _assetStore.StateChanged += FetchAssets;

            FetchAssets();
        }


        public AssetViewModel(
            AssetStore assetStore,
            IAssetService assetService,
            Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filteredAssets )
        {
            _assetStore = assetStore;
            _assetService = assetService;
            _filteredAssets = filteredAssets;

            _assets = new ObservableCollection<AssetModel>();
            _assetStore.StateChanged += FetchAssetWatchlist;

            FetchAssetWatchlist();
        }


        public AssetViewModel(
            AssetStore assetStore,
            Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filteredAssets )
        {
            _assetStore = assetStore;
            _filteredAssets = filteredAssets;

            _assets = new ObservableCollection<AssetModel>();
            _assetStore.StateChanged += FetchAssets;

            FetchAssets();
        }


        private void FetchAssets()
        {
            IEnumerable<AssetModel> assets = _assetStore.AssetsList
                .Select( a => new AssetModel( a.ID, a.Asset, a.PurchasePrice, a.QuantityOwned, a.Category, a.PurchaseDate, _accountStore, _assetService, _renavigator ) );

            assets = AddAssetToList( assets );
        }


        private void FetchAssetWatchlist()
        {
            List<AssetModel> assetWatchlist = _assetStore.AssetsList
                .Select( a => new AssetModel( a.Asset, _assetService ) )
                .ToList();

            foreach ( AssetModel asset in assetWatchlist )
            {
                ConstructSeries( asset );
            }

            assetWatchlist = AddAssetToList( assetWatchlist ).ToList();
        }


        private async Task ConstructSeries( AssetModel asset )
        {
            IEnumerable<HistoricalData> data = await _assetService.FetchCryptoHistoricalData( asset.Asset.ID );

            if ( data.IsNullOrEmpty() )
                return;

            asset.Series[0].Values = new ObservableCollection<double>( data.Select( d => d.Price ) );
        }


        private IEnumerable<AssetModel> AddAssetToList( IEnumerable<AssetModel> assets )
        {
            assets = _filteredAssets( assets );

            _assets.Clear();
            foreach ( AssetModel asset in assets )
            {
                _assets.Add( asset );
            }

            ListEmpty = _assets.IsNullOrEmpty();

            PropertyChanged += PropertyChangedEventHandler;

            return assets;
        }


        private void SortAssets( SortOptions selectedOption )
        {
            Type type = typeof( AssetModel );
            PropertyInfo? property = type.GetProperty( selectedOption.Property );

            if ( property == null )
                return;

            IEnumerable<AssetModel> sortedAssets = new ObservableCollection<AssetModel>( _assets );

            sortedAssets = selectedOption.Direction == Enums.SortDirection.Ascending
                ? sortedAssets.OrderBy( a => property.GetValue( a ) )
                : sortedAssets.OrderByDescending( a => property.GetValue( a ) );

            _assets.Clear();
            foreach ( AssetModel asset in sortedAssets )
            {
                _assets.Add( asset );
            }
        }


        private void PropertyChangedEventHandler( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( SelectedOption ) )
                SortAssets( SelectedOption );
        }
    }
}
