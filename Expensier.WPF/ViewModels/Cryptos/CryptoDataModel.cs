using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.Commands;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels.Cryptos
{
    public class CryptoDataModel : ViewModelBase
    {
        private readonly ICryptoService _cryptoService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public int Id { get; set; }
        public Crypto Crypto { get; set; }
        public double PurchasePrice { get; set; }
        public double Amount { get; set; }
        public double TotalValue { get; set; }
        public ICommand DeleteCommand { get; }

        private ChartValues<double> _cryptoPerformance;
        public ChartValues<double> CryptoPerformance
        {
            get
            {
                return _cryptoPerformance;
            }
            set
            {
                _cryptoPerformance = value;
                OnPropertyChanged(nameof(CryptoPerformance));
            }
        }

        public ColorsCollection SeriesColors { get; }

        /// <summary>
        /// Crypto asset list item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="crypto"></param>
        /// <param name="purchasePrice"></param>
        /// <param name="amount"></param>
        /// <param name="cryptoService"></param>
        /// <param name="accountStore"></param>
        /// <param name="renavigator"></param>
        public CryptoDataModel(
            int id,
            Crypto crypto,
            double purchasePrice,
            double amount, 
            ICryptoService cryptoService,
            AccountStore accountStore,
            IRenavigator renavigator)
        {
            Id = id;
            Crypto = crypto;
            PurchasePrice = purchasePrice;
            Amount = amount;

            TotalValue = (double)(amount * Crypto.Price);

            _cryptoService = cryptoService;
            _accountStore = accountStore;
            _renavigator = renavigator;

            DeleteCommand = new DeleteCryptoCommand(this, cryptoService, accountStore, renavigator);
        }

        /// <summary>
        /// Crypto watch list item.
        /// </summary>
        /// <param name="crypto"></param>
        /// <param name="cryptoService"></param>
        public CryptoDataModel(Crypto crypto, ICryptoService cryptoService)
        {
            Crypto = crypto;
            _cryptoService = cryptoService;

            CryptoPerformance = new ChartValues<double>();
            SeriesColors = new ColorsCollection();

            ConstructSeries(_cryptoService, Crypto.Symbol);
            AddChartColors(Crypto.ChangesPercentage);
        }

        private async Task ConstructSeries(ICryptoService cryptoService, string symbol)
        {
            IEnumerable<PriceData> cryptoPrices = await cryptoService.GetHistoricalPrices(symbol);
            
            CryptoPerformance = new ChartValues<double>(cryptoPrices
                .Select(c => c.Close));
        }

        private void AddChartColors(double? changesPercentage)
        {
            SeriesColors.Clear();
            SeriesColors.AddRange(new[]
            {
                changesPercentage >= 0 ? "#64927C" : "#D94E33"
            }
            .Select(ColorConverter.ConvertFromString)
            .OfType<Color>()
            .ToList());
        }
    }
}
