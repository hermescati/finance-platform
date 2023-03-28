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
    public class TopPerformingCryptosViewModel : CryptoWatchlistBaseViewModel
    {
        public TopPerformingCryptosViewModel(CryptoStore cryptoStore, ICryptoService cryptoService) 
            : base(cryptoStore, cryptoService, cryptos => cryptos.OrderByDescending(c => c.Crypto.ChangesPercentage)) { }
    }
}
