using Expensier.Domain.Services;
using Expensier.WPF.State.Crypto;
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
        private readonly CryptoStore _cryptoStore;
        private readonly ICryptoService _cryptoService;
        public CryptoViewModel CryptoViewModel { get; }
        private readonly ObservableCollection<CryptoDataModel> _cryptos;

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

        public IEnumerable<CryptoDataModel> Cryptos => _cryptos;

        public CryptoWatchlistBaseViewModel(
            CryptoStore cryptoStore, 
            ICryptoService cryptoService, 
            Func<IEnumerable<CryptoDataModel>, IEnumerable<CryptoDataModel>> filterCryptos)
        {
            _cryptos = new ObservableCollection<CryptoDataModel>();
            _cryptoStore = cryptoStore;
            _cryptoService = cryptoService;

            CryptoViewModel = new CryptoViewModel(cryptoStore, filterCryptos, cryptoService);

            _cryptos = (ObservableCollection<CryptoDataModel>)CryptoViewModel.Cryptos;

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
