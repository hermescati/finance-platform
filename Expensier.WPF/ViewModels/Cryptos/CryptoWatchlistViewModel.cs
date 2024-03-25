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
    public class CryptoWatchlistViewModel : CryptoWatchlistBaseViewModel
    {
        public CryptoWatchlistViewModel(CryptoStore cryptoStore, ICryptoService cryptoService)
            : base(cryptoStore, cryptoService, cryptos => cryptos.OrderByDescending(c => c.Crypto.PercentageChange)) { }
    }
}
