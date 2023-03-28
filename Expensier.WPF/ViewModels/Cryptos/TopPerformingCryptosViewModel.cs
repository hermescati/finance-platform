using Expensier.WPF.State.Crypto;
using Microsoft.IdentityModel.Tokens;
using System;
using LiveCharts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using Expensier.API.Services;
using Expensier.Domain.Services;
using Expensier.Domain.Models;

namespace Expensier.WPF.ViewModels.Cryptos
{
    public class TopPerformingCryptosViewModel : ViewModelBase
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

        public TopPerformingCryptosViewModel(CryptoStore cryptoStore, ICryptoService cryptoService)
        {
            _cryptos = new ObservableCollection<CryptoDataModel>();
            _cryptoStore = cryptoStore;
            _cryptoService = cryptoService;

            CryptoViewModel = new CryptoViewModel(cryptoStore,
                cryptos => cryptos
                .OrderByDescending(c => c.Crypto.ChangesPercentage)
                .Take(4),
                cryptoService);

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
