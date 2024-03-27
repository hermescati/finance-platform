using Expensier.Domain.Models;
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


        AssetTransaction GetAssetBySymbol(
            Account currentAccount,
            string symbol );


        Task<Account> DeleteAsset(
            Account currentAccount,
            Guid assetID );


        Task<Asset> FetchCryptoAsset( string cryptoSymbol );

        Task<IEnumerable<HistoricalData>> FetchCryptoHistoricalData (string cryptoSymbol);
    }
}
