using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Windows.Input;
using Expensier.WPF.Commands.Assets;
using Expensier.WPF.ViewModels;
using static Expensier.Domain.Models.AssetTransaction;
using LiveChartsCore;
using Expensier.WPF.Utils;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Measure;


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
        public double TotalValue { get; set; }
        public ICommand DeleteCommand { get; }


        public ISeries[] Series { get; set; }
        public Axis[] XAxis { get; set; }
        public Axis[] YAxis { get; set; }
        public Margin Margin { get; set; }


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

            TotalValue = (double) QuantityOwned * Asset.CurrentPrice;

            _accountStore = accountStore;
            _assetService = assetService;
            _renavigator = renavigator;

            DeleteCommand = new DeleteAssetCommand( this, _assetService,  _renavigator, _accountStore );
        }


        public AssetModel( Asset asset, IAssetService assetService )
        {
            Asset = asset;
            _assetService = assetService;

            Series = new ISeries[]
            {
                asset.PercentageChange < 0 ? ChartSettings.AssetLineSeries(negative: true) : ChartSettings.AssetLineSeries()
            };
            XAxis = new Axis[] { ChartSettings.DefaultXAxis( showAxis: false ) };
            YAxis = new Axis[] { ChartSettings.DefaultYAxis( showAxis: false ) };
            Margin = new Margin( -120, 0, 0, 0 );
        }
    }
}
