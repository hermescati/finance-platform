using Expensier.Domain.Models;


namespace Expensier.Domain.Services.Portfolio
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IDataService<Account> _accountService;
        private readonly IAssetService _assetService;


        public PortfolioService( IDataService<Account> accountService, IAssetService assetService )
        {
            _accountService = accountService;
            _assetService = assetService;
        }


        //public async Task<Account> StoreReturns( Account currentAccount )
        //{
        //    double portfolioReturn = await GetPortfolioReturn( currentAccount );

        //    PortfolioReturn newReturn = new PortfolioReturn()
        //    {
        //        AccountHolder = currentAccount,
        //        RecordedDate = DateTime.Now,
        //        ReturnPercentage = portfolioReturn
        //    };

        //    currentAccount.PortfolioReturn.Add( newReturn );

        //    await _accountService.Update( currentAccount.ID, currentAccount );

        //    return currentAccount;
        //}

        //public async Task<double> GetPortfolioReturn( Account currentAccount )
        //{
        //    double portfolioReturn = 0;
        //    IEnumerable<AssetTransaction> cryptoAssets = currentAccount.AssetList;

        //    //foreach (AssetTransaction asset in cryptoAssets)
        //    //{
        //    //    double cryptoReturn = await _cryptoService.GetCryptoReturns(asset);
        //    //    double cryptoWeight = GetCryptoWeight(currentAccount, asset);

        //    //    portfolioReturn += (cryptoReturn * cryptoWeight);
        //    //}

        //    return portfolioReturn;
        //}


        public double FindTotalValue( IEnumerable<AssetTransaction> assets )
        {
            return assets.Sum( asset => asset.QuantityOwned * asset.Asset.CurrentPrice );
        }


        public double FindInitialInvestment( IEnumerable<AssetTransaction> assets )
        {
            return assets.Sum( asset => asset.QuantityOwned * asset.PurchasePrice );
        }
    }
}
