using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.Commands.Assets;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Errors;
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

        private double _coins;
        public double Coins
        {
            get
            {
                return _coins;
            }
            set
            {
                _coins = value;
                OnPropertyChanged(nameof(Coins));
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

        private bool _validSymbol = false;
        public bool ValidSymbol
        {
            get
            {
                return _validSymbol;
            }
            set
            {
                _validSymbol = value;
                OnPropertyChanged(nameof(ValidSymbol));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public bool CanAdd => !string.IsNullOrEmpty(CryptoSymbol) && 
            Coins > 0.0 && 
            PurchasePrice > 0.0;

        private Asset _crypto;
        public Asset Crypto
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
            ErrorMessageViewModel = new MessageViewModel();

            SearchSymbolCommand = new SearchCryptoCommand(this, cryptoService);
            AddCryptoCommand = new AddCryptoCommand(this, cryptoService, accountStore, renavigator);
        }
    }
}
