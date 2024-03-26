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
        private readonly IDataService<AssetTransaction> _cryptoService;


        public AssetService(
            APIClient client,
            IDataService<Account> accountService,
            IDataService<AssetTransaction> cryptoService )
        {
            _client = client;
            _accountService = accountService;
            _cryptoService = cryptoService;
        }


        public async Task<Account> AddAsset(
            Account currentAccount,
            Asset asset,
            double purchasePrice,
            double quantityOwned,
            AssetType category,
            DateTime purchaseDate )
        {
            AssetTransaction? existingAsset = GetAssetBySymbol( currentAccount, asset.Symbol );

            if ( existingAsset == null )
            {
                AssetTransaction newAsset = new AssetTransaction()
                {
                    User = currentAccount,
                    Asset = asset,
                    PurchasePrice = purchasePrice,
                    QuantityOwned = quantityOwned,
                    Category = category,
                    PurchaseDate = purchaseDate
                };

                currentAccount.AssetList?.Add( newAsset );
                await _accountService.Update( currentAccount.ID, currentAccount );
            }

            return currentAccount;
        }


        public AssetTransaction GetAssetByID( Account currentAccount, Guid assetID ) => currentAccount.AssetList
            .Single( ( a ) => a.ID == assetID );


        public AssetTransaction? GetAssetBySymbol( Account currentAccount, string symbol ) => currentAccount?.AssetList
            .FirstOrDefault( ( a ) => a.Asset.Symbol == symbol );


        public async Task<Account> DeleteAsset( Account currentAccount, Guid assetID )
        {
            AssetTransaction assetToDelete = GetAssetByID( currentAccount, assetID );

            if ( assetToDelete != null )
            {
                currentAccount.AssetList?.Remove( assetToDelete );

                await _accountService.Update( currentAccount.ID, currentAccount );
                await _cryptoService.Delete( assetID );
            }

            return currentAccount;
        }


        public async Task<Asset> FetchCryptoAsset( string cryptoSymbol )
        {
            string URI = $"coins/{cryptoSymbol}"; ;
            Asset cryptoAsset = await _client.GetCryptoAsset( URI );

            if ( cryptoAsset == null || cryptoAsset.CurrentPrice == 0 )
                throw new InvalidSymbolException( cryptoSymbol );

            return cryptoAsset;
        }
    }
}
