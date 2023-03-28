using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Crypto;
using Expensier.WPF.State.Navigators;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Cryptos
{
    public class CryptoViewModel : ViewModelBase
    {
        private readonly ICryptoService _cryptoService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;
        private readonly CryptoStore _cryptoStore;
        private readonly Func<IEnumerable<CryptoDataModel>, IEnumerable<CryptoDataModel>> _filterCryptos;
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

        public CryptoViewModel(
            CryptoStore cryptoStore,
            ICryptoService cryptoService,
            AccountStore accountStore,
            IRenavigator renavigator) : this(cryptoStore, cryptos => cryptos, cryptoService, accountStore, renavigator) { }


        public CryptoViewModel(
            CryptoStore cryptoStore, 
            Func<IEnumerable<CryptoDataModel>, IEnumerable<CryptoDataModel>> filterCryptos,
            ICryptoService cryptoService,
            AccountStore accountStore, 
            IRenavigator renavigator)
        {
            _cryptoStore = cryptoStore;
            _cryptoService = cryptoService;
            _accountStore = accountStore;
            _renavigator = renavigator;
            _filterCryptos = filterCryptos;
            _cryptos = new ObservableCollection<CryptoDataModel>();

            _cryptoStore.StateChanged += Crypto_StateChanged;

            ResetCryptos();
        }

        public CryptoViewModel(
            CryptoStore cryptoStore, 
            Func<IEnumerable<CryptoDataModel>, IEnumerable<CryptoDataModel>> filterCryptos,
            ICryptoService cryptoService)
        {
            _cryptoStore = cryptoStore;
            _cryptoService = cryptoService;
            _filterCryptos = filterCryptos;
            _cryptos = new ObservableCollection<CryptoDataModel>();

            _cryptoStore.StateChanged += Crypto_StateChanged;

            ResetCryptos();
        }

        private void ResetCryptos()
        {
            IEnumerable<CryptoDataModel> cryptoDataModel = _cryptoStore.CryptoAssetList
                .Select(c => new CryptoDataModel(c.Id, c.Crypto, c.Purchase_Price, c.Amount, _cryptoService, _accountStore, _renavigator));

            cryptoDataModel = _filterCryptos(cryptoDataModel);

            _cryptos.Clear();
            foreach (CryptoDataModel dataModel in cryptoDataModel)
            {
                _cryptos.Add(dataModel);
            }

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

        private void Crypto_StateChanged()
        {
            ResetCryptos();
        }
    }
}
