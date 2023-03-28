using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.Commands;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels.Modals
{
    public class CryptoModalViewModel : ViewModelBase
    {
        private string _cryptoSymbol;
        public string CryptoSymbol
        {
            get
            {
                return _cryptoSymbol;
            }
            set
            {
                _cryptoSymbol = value;
                OnPropertyChanged(nameof(CryptoSymbol));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        private double _amount;
        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        private double _purchasePrice;
        public double PurchasePrice
        {
            get
            {
                return _purchasePrice;
            }
            set
            {
                _purchasePrice = value;
                OnPropertyChanged(nameof(PurchasePrice));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        public bool CanAdd => !string.IsNullOrEmpty(CryptoSymbol) && 
            Amount > 0.0 && 
            PurchasePrice > 0.0;

        private Crypto _crypto;
        public Crypto Crypto
        {
            get
            {
                return _crypto;
            }
            set
            {
                _crypto = value;
                OnPropertyChanged(nameof(Crypto));
            }
        }

        public ICommand SearchSymbolCommand { get; }
        public ICommand AddCryptoCommand { get; }

        public CryptoModalViewModel(ICryptoService cryptoService, AccountStore accountStore, IRenavigator renavigator)
        {
            SearchSymbolCommand = new SearchCryptoCommand(this, cryptoService);
            AddCryptoCommand = new AddCryptoCommand(this, cryptoService, accountStore, renavigator);
        }
    }
}
