using Expensier.Domain.Models;
using System.Collections.Generic;


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


        //public async Task<Dictionary<string, double>> GetPorfolioDetails( Account currentAccount )
        //{
        //    Dictionary<string, double> portfolioData = new Dictionary<string, double>();

        //    IEnumerable<AssetTransaction> assets = currentAccount.AssetList;
        //    IEnumerable<AssetTransaction> cryptoAssets = assets
        //        .Where( a => a.Category == AssetTransaction.AssetType.Cryptocurrency )
        //        .Take( 1 );

        //    double portfolioValue = FindTotalValue( assets );
        //    portfolioData["portfolioValue"] = portfolioValue;

        //    double cryptosValue = FindTotalValue( cryptoAssets );
        //    portfolioData["cryptosValue"] = cryptosValue;

        //    double cryptosInvestment = FindInitialInvestment( cryptoAssets );
        //    portfolioData["cryptosInvestment"] = cryptosInvestment;

        //    Task<double> cryptoROITask = FindROI( cryptoAssets, portfolioValue );
        //    await Task.WhenAll( cryptoROITask );
        //    portfolioData["cryptosROI"] = cryptoROITask.Result;

        //    return portfolioData;
        //}


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


        public async Task<double> FindROI( IEnumerable<AssetTransaction> assets, double portfolioValue )
        {
            double roi = 0;

            foreach ( AssetTransaction asset in assets )
            {
                double assetWeight = ( asset.Asset.CurrentPrice * asset.QuantityOwned ) / portfolioValue;
                double assetROI = await _assetService.GetROI( asset );

                roi += ( assetWeight * assetROI );
            }

            return roi;
        }
    }
}
