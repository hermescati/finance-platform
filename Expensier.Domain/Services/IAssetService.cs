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


        AssetTransaction GetAssetTransactionByID(
            Account currentAccount,
            Guid assetTransactionID );


        AssetTransaction GetAssetByID(
            Account currentAccount,
            string assetID );


        Task<Account> DeleteAsset(
            Account currentAccount,
            Guid assetTransactionID );


        Task<Asset> GetAsset( string cryptoID );


        Task<IEnumerable<HistoricalData>> GetHistoricalData( string cryptoID );


        Task<double> GetROI( AssetTransaction asset );
    }
}
