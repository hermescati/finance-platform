﻿using Expensier.Domain.Models;
using static Expensier.Domain.Models.AssetTransaction;


namespace Expensier.Domain.Services
{
    public interface IAssetService
    {
        Task<Account> AddAsset(
            Account currentAccount,
            Asset asset,
            double purchasePrice,
            double quantityOwned,
            AssetType category,
            DateTime purchaseDate );


        AssetTransaction GetAssetByID(
            Account currentAccount,
            Guid assetID );


        Asset GetAssetBySymbol(
            Account currentAccount,
            string symbol );


        Task<Account> DeleteAsset(
            Account currentAccount,
            Guid assetID );


        Task<Asset> GetCrypto( string symbol );

        Task<IEnumerable<PriceData>> GetHistoricalPrices( string symbol );

        public double GetMarketValue( double? price, double coins );

        Task<double> GetCryptoReturns( AssetTransaction currentCrypto );

        Task<Account> AddCrypto(
    Account currentAccount,
    Asset currentCrypto,
    double purchasePrice,
    double amount );
    }
}
