using Expensier.Domain.Services;
using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Assets;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Cryptos
{
    public abstract class CryptoWatchlistBaseViewModel : ViewModelBase
    {
        private readonly AssetStore _cryptoStore;
        private readonly IAssetService _cryptoService;
        public CryptoViewModel CryptoViewModel { get; }
        private readonly ObservableCollection<AssetModel> _cryptos;

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

        public IEnumerable<AssetModel> Cryptos => _cryptos;

        public CryptoWatchlistBaseViewModel(
            AssetStore cryptoStore, 
            IAssetService cryptoService, 
            Func<IEnumerable<AssetModel>, IEnumerable<AssetModel>> filterCryptos)
        {
            _cryptos = new ObservableCollection<AssetModel>();
            _cryptoStore = cryptoStore;
            _cryptoService = cryptoService;

            CryptoViewModel = new CryptoViewModel(cryptoStore, filterCryptos, cryptoService);

            _cryptos = (ObservableCollection<AssetModel>)CryptoViewModel.Assets;

            if (_cryptos.IsNullOrEmpty())
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listEmpty = false;
                _listNotEmpty = true;
            }
        }
    }
}
