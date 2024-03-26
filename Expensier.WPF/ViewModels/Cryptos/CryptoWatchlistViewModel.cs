using Expensier.Domain.Services;
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
    public class CryptoWatchlistViewModel : CryptoWatchlistBaseViewModel
    {
        public CryptoWatchlistViewModel(AssetStore cryptoStore, IAssetService cryptoService)
            : base(cryptoStore, cryptoService, cryptos => cryptos.OrderByDescending(c => c.Asset.PercentageChange)) { }
    }
}
