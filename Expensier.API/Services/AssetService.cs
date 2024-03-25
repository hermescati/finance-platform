﻿using Expensier.Domain.Services;
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
            Asset existingAsset = GetAssetBySymbol( currentAccount, asset.Symbol );

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


        public Asset GetAssetBySymbol( Account currentAccount, string symbol ) => currentAccount.AssetList
            .FirstOrDefault( ( a ) => a.Asset.Symbol == symbol ).Asset;


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


        /// <summary>
        /// API call to retrieve market data for the provided crypto asset.
        /// </summary>
        /// <param name="symbol">The symbol of the crypto asset (symbolUSD)</param>
        /// <returns>The crypto asset with market data</returns>
        /// <exception cref="InvalidSymbolException">Thrown if the provided symbol does not belong to any crypto asset.</exception>
        public async Task<Asset> GetCrypto( string symbol )
        {
            var uri = "quote/" + symbol;
            Asset crypto = await _client.DeserializeResponse<Asset>( uri );

            if ( crypto == null || crypto.CurrentPrice == 0 )
            {
                throw new InvalidSymbolException( symbol );
            }

            return crypto;
        }

        /// <summary>
        /// API call to retrieve historical prices of the provided crypto asset.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        /// <exception cref="InvalidSymbolException"></exception>
        public async Task<IEnumerable<PriceData>> GetHistoricalPrices( string symbol )
        {
            var uri = "historical-chart/1hour/" + symbol;
            var cryptoPrices = await _client.DeserializeHistoricalPrices( uri );

            foreach ( PriceData crypto in cryptoPrices )
            {
                if ( crypto.Close == 0 )
                {
                    throw new InvalidSymbolException( symbol );
                }
            }

            return cryptoPrices.Where( price => price.Date.Date == DateTime.Now.Date );
        }

        public double GetMarketValue( double? price, double coins )
        {
            throw new NotImplementedException();
        }

        public Task<double> GetCryptoReturns( AssetTransaction currentCrypto )
        {
            throw new NotImplementedException();
        }

        public Task<Account> AddCrypto( Account currentAccount, Asset currentCrypto, double purchasePrice, double amount )
        {
            throw new NotImplementedException();
        }
    }
}
