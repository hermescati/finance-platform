using Expensier.Domain.Services;
using Expensier.Domain.Exceptions;
using Expensier.Domain.Models;
using static Expensier.Domain.Models.AssetTransaction;


namespace Expensier.API.Services
{
    public class AssetService : IAssetService
    {
        private readonly APIClient _client;
        private readonly IDataService<Account> _accountService;
        private readonly IDataService<AssetTransaction> _assetService;


        public AssetService(
            APIClient client,
            IDataService<Account> accountService,
            IDataService<AssetTransaction> assetService )
        {
            _client = client;
            _accountService = accountService;
            _assetService = assetService;
        }


        public async Task<Account> AddAsset(
            Account currentAccount,
            Asset asset,
            double purchasePrice,
            double quantityOwned,
            AssetType category,
            DateTime purchaseDate )
        {
            AssetTransaction? existingAsset = GetAssetByID( currentAccount, asset.ID );

            if ( existingAsset == null )
            {
                AssetTransaction newAsset = new()
                {
                    User = currentAccount,
                    Asset = asset,
                    PurchasePrice = purchasePrice,
                    QuantityOwned = quantityOwned,
                    Category = category,
                    PurchaseDate = purchaseDate
                };

                await _assetService.GetByID( newAsset.ID );
                currentAccount.AssetList?.Add( newAsset );
                await _accountService.Update( currentAccount.ID, currentAccount );
            }

            return currentAccount;
        }


        public AssetTransaction GetAssetTransactionByID( Account currentAccount, Guid assetTransactionID ) => currentAccount.AssetList
            .Single( ( a ) => a.ID == assetTransactionID );


        public AssetTransaction GetAssetByID( Account currentAccount, string assetID ) => currentAccount?.AssetList
            .FirstOrDefault( ( a ) => a.Asset.Symbol == assetID );


        public async Task<Account> DeleteAsset( Account currentAccount, Guid assetTransactionID )
        {
            AssetTransaction assetToDelete = GetAssetTransactionByID( currentAccount, assetTransactionID );

            if ( assetToDelete != null )
            {
                currentAccount.AssetList?.Remove( assetToDelete );

                await _accountService.Update( currentAccount.ID, currentAccount );
                await _assetService.Delete( assetTransactionID );
            }

            return currentAccount;
        }


        public async Task<Asset> GetAsset( string cryptoID )
        {
            string URI = $"coins/{cryptoID}";
            Asset cryptoAsset = await _client.FetchCryptoAsset( URI );

            if ( cryptoAsset == null || cryptoAsset.CurrentPrice == 0 )
                throw new InvalidSymbolException( cryptoID );

            return cryptoAsset;
        }


        public async Task<IEnumerable<HistoricalData>> GetHistoricalData( string cryptoID )
        {
            string URI = $"coins/{cryptoID}/market_chart?vs_currency=usd&days={2}";
            IEnumerable<HistoricalData> historicalData = await _client.FetchCryptoHistoricalData( URI );

            if ( historicalData == null )
                return default;

            return historicalData;
        }


        public async Task<double> GetROI( AssetTransaction asset )
        {
            string? formattedDate = asset.PurchaseDate?.ToString( "dd-MM-yyyy" );
            string URI = $"coins/{asset.Asset.ID}/history?date={formattedDate}&localization=false";
            double historicalPrice = await _client.FetchCryptoHistoricalPrice( URI );

            if ( historicalPrice == 0 )
                return default;

            return ( asset.Asset.CurrentPrice / historicalPrice - 1 );
        }
    }
}
