using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Crypto;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Cryptos;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        public CryptoViewModel CryptoViewModel { get; }
        public CryptoWatchlistViewModel CryptoWatchlistViewModel { get; }
        public CryptoModalViewModel CryptoModalViewModel { get; }

        public WalletViewModel(
            CryptoWatchlistViewModel cryptoWatchlistViewModel, 
            CryptoStore cryptoStore,
            CryptoModalViewModel cryptoModalViewModel,
            ICryptoService cryptoService,
            AccountStore accountStore,
            IRenavigator renavigator)
        {
            CryptoWatchlistViewModel= cryptoWatchlistViewModel;
            CryptoViewModel = new CryptoViewModel(cryptoStore, cryptoService, accountStore, renavigator);
            CryptoModalViewModel= cryptoModalViewModel;
        }
    }
}
