using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<double> GetPortfolioReturn( Account currentAccount )
        {
            double portfolioReturn = 0;
            IEnumerable<AssetTransaction> cryptoAssets = currentAccount.AssetList;

            //foreach (AssetTransaction asset in cryptoAssets)
            //{
            //    double cryptoReturn = await _cryptoService.GetCryptoReturns(asset);
            //    double cryptoWeight = GetCryptoWeight(currentAccount, asset);

            //    portfolioReturn += (cryptoReturn * cryptoWeight);
            //}

            return portfolioReturn;
        }

        public double GetPortfolioValue( Account currentAccount )
        {
            double marketValue = 0;
            IEnumerable<AssetTransaction>? assets = currentAccount.AssetList;

            if ( assets?.Any() != true )
            {
                return marketValue;
            }

            foreach ( AssetTransaction asset in assets )
            {
                marketValue += _assetService.GetAssetValue( asset.QuantityOwned, asset.Asset.CurrentPrice );
            }

            return marketValue;
        }

        public double GetCryptoWeight( Account currentAccount, AssetTransaction currentCrypto )
        {
            double portfolioMarketValue = GetPortfolioValue( currentAccount );
            //double cryptoMarketValue = _cryptoService.GetMarketValue(currentCrypto.Asset.CurrentPrice, currentCrypto.QuantityOwned );

            return portfolioMarketValue;
        }

        public async Task<Account> StoreReturns( Account currentAccount )
        {
            double portfolioReturn = await GetPortfolioReturn( currentAccount );

            PortfolioReturn newReturn = new PortfolioReturn()
            {
                AccountHolder = currentAccount,
                RecordedDate = DateTime.Now,
                ReturnPercentage = portfolioReturn
            };

            currentAccount.PortfolioReturn.Add( newReturn );

            await _accountService.Update( currentAccount.ID, currentAccount );

            return currentAccount;
        }
    }
}
