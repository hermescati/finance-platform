using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Expensier.WPF.Commands.Assets;
using Expensier.WPF.ViewModels;
using static Expensier.Domain.Models.AssetTransaction;


namespace Expensier.WPF.DataObjects
{
    public class AssetModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly IAssetService _assetService;
        private readonly IRenavigator _renavigator;


        public Guid ID { get; set; }
        public Asset Asset { get; set; }
        public double PurchasePrice { get; set; }
        public double QuantityOwned { get; set; }
        public AssetType Category { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public ICommand DeleteCommand { get; }


        public AssetModel(
            Guid id,
            Asset asset,
            double purchasePrice,
            double quantityOwned,
            AssetType category,
            DateTime? purchaseDate,
            AccountStore accountStore,
            IAssetService assetService,
            IRenavigator renavigator )
        {
            ID = id;
            Asset = asset;
            PurchasePrice = purchasePrice;
            QuantityOwned = quantityOwned;
            Category = category;
            PurchaseDate = purchaseDate;

            _accountStore = accountStore;
            _assetService = assetService;
            _renavigator = renavigator;

            DeleteCommand = new DeleteCryptoCommand( this, assetService, accountStore, renavigator );
        }

        /// <summary>
        /// Crypto watch list item.
        /// </summary>
        /// <param name="crypto"></param>
        /// <param name="cryptoService"></param>
        public AssetModel( Asset crypto, IAssetService cryptoService )
        {
            Asset = crypto;
            _assetService = cryptoService;

            CryptoPerformance = new ChartValues<double>();
            SeriesColors = new ColorsCollection();

            ConstructSeries( _assetService, Asset.Symbol );
            AddChartColors( Asset.PercentageChange );
        }


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
                OnPropertyChanged( nameof( CryptoPerformance ) );
            }
        }

        public ColorsCollection SeriesColors { get; }

        private async Task ConstructSeries( IAssetService cryptoService, string symbol )
        {
            IEnumerable<PriceData> cryptoPrices = await cryptoService.GetHistoricalPrices( symbol );

            CryptoPerformance = new ChartValues<double>( cryptoPrices
                .Select( c => c.Close ) );
        }

        private void AddChartColors( double? changesPercentage )
        {
            SeriesColors.Clear();
            SeriesColors.AddRange( new[]
            {
                changesPercentage >= 0 ? "#64927C" : "#D94E33"
            }
            .Select( ColorConverter.ConvertFromString )
            .OfType<Color>()
            .ToList() );
        }
    }
}
