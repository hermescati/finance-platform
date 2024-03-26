using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
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

            DeleteCommand = new DeleteCryptoCommand( this, _assetService, _accountStore, _renavigator );
        }
    }
}
