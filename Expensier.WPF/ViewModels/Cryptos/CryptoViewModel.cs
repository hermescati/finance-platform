using Expensier.Domain.Services;
using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Crypto;
using Expensier.WPF.State.Navigators;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Expensier.WPF.ViewModels.Cryptos
{
    public class CryptoViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly CryptoStore _cryptoStore;
        private readonly IAssetService _assetService;
        private readonly IRenavigator _renavigator;
        private readonly Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> _filterAssets;


        private readonly ObservableCollection<AssetModel> _assets;
        public IEnumerable<AssetModel> Assets => _assets;



        private bool _listEmpty;
        public bool ListEmpty
        {
            get
            {
                return _listEmpty;
            }
            set
            {
                _listEmpty = value;
                OnPropertyChanged(nameof(ListEmpty));
            }
        }

        private bool _listNotEmpty;
        public bool ListNotEmpty
        {
            get
            {
                return _listNotEmpty;
            }
            set
            {
                _listNotEmpty = value;
                OnPropertyChanged(nameof(ListNotEmpty));
            }
        }



        public CryptoViewModel(
            CryptoStore cryptoStore,
            IAssetService cryptoService,
            AccountStore accountStore,
            IRenavigator renavigator) : this(cryptoStore, cryptos => cryptos, cryptoService, accountStore, renavigator) { }


        public CryptoViewModel(
            CryptoStore cryptoStore, 
            Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filterCryptos,
            IAssetService cryptoService,
            AccountStore accountStore, 
            IRenavigator renavigator)
        {
            _cryptoStore = cryptoStore;
            _assetService = cryptoService;
            _accountStore = accountStore;
            _renavigator = renavigator;
            _filterAssets = filterCryptos;
            _assets = new ObservableCollection<AssetModel>();

            _cryptoStore.StateChanged += CryptoList_StateChanged;

            ResetCryptosList();
        }

        public CryptoViewModel(
            CryptoStore cryptoStore, 
            Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filterCryptos,
            IAssetService cryptoService)
        {
            _cryptoStore = cryptoStore;
            _assetService = cryptoService;
            _filterAssets = filterCryptos;
            _assets = new ObservableCollection<AssetModel>();

            _cryptoStore.StateChanged += CryptoWatchlist_StateChanged;

            ResetCryptosWatchlist();
        }


        public CryptoViewModel(CryptoStore cryptoStore, Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filterCryptos)
        {
            _cryptoStore = cryptoStore;
            _filterAssets = filterCryptos;
            _assets = new ObservableCollection<AssetModel>();

            _cryptoStore.StateChanged += CryptoList_StateChanged;

            ResetCryptosList();
        }


        private void ResetCryptosList()
        {
            IEnumerable<AssetModel> cryptoDataModel = _cryptoStore.CryptoAssetList
                .Select(c => new AssetModel(c.ID, c.Asset, c.PurchasePrice, c.QuantityOwned, c.Category, c.PurchaseDate,_accountStore, _assetService, _renavigator));
            
            cryptoDataModel = AddCryptoToList(cryptoDataModel);
        }

        private void ResetCryptosWatchlist()
        {
            IEnumerable<AssetModel> cryptoDataModel = _cryptoStore.CryptoAssetList
                .Select(c => new AssetModel(c.Asset, _assetService));

            cryptoDataModel = AddCryptoToList(cryptoDataModel);
        }


        private IEnumerable<AssetModel> AddCryptoToList(IEnumerable<AssetModel> cryptoDataModel)
        {
            cryptoDataModel = _filterAssets(cryptoDataModel);

            _assets.Clear();
            foreach (AssetModel dataModel in cryptoDataModel)
            {
                _assets.Add(dataModel);
            }

            if (_assets.IsNullOrEmpty())
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listEmpty = false;
                _listNotEmpty = true;
            }

            return cryptoDataModel;
        }


        private void CryptoList_StateChanged()
        {
            ResetCryptosList();
        }

        private void CryptoWatchlist_StateChanged()
        {
            ResetCryptosWatchlist();
        }
    }
}
