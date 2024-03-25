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
        private readonly ICryptoService _cryptoService;

        public PortfolioService(IDataService<Account> accountService, ICryptoService cryptoService)
        {
            _accountService = accountService;
            _cryptoService = cryptoService;
        }

        public async Task<double> GetPortfolioReturn(Account currentAccount)
        {
            double portfolioReturn = 0;
            IEnumerable<AssetTransaction> cryptoAssets = currentAccount.AssetList;

            foreach (AssetTransaction asset in cryptoAssets)
            {
                double cryptoReturn = await _cryptoService.GetCryptoReturns(asset);
                double cryptoWeight = GetCryptoWeight(currentAccount, asset);

                portfolioReturn += (cryptoReturn * cryptoWeight);
            }

            return portfolioReturn;
        }

        public double GetMarketValue(Account currentAccount)
        {
            double marketValue = 0;
            IEnumerable<AssetTransaction> cryptoAssets = currentAccount.AssetList;
            
            foreach(AssetTransaction asset in cryptoAssets)
            {
                marketValue += _cryptoService.GetMarketValue(asset.Asset.CurrentPrice, asset.QuantityOwned);
            }

            return marketValue;
        }

        public double GetCryptoWeight(Account currentAccount, AssetTransaction currentCrypto)
        {
            double portfolioMarketValue = GetMarketValue(currentAccount);
            double cryptoMarketValue = _cryptoService.GetMarketValue(currentCrypto.Asset.CurrentPrice, currentCrypto.QuantityOwned );

            return cryptoMarketValue / portfolioMarketValue;
        }

        public async Task<Account> StoreReturns(Account currentAccount)
        {
            double portfolioReturn = await GetPortfolioReturn(currentAccount);

            PortfolioReturn newReturn = new PortfolioReturn()
            {
                AccountHolder = currentAccount,
                RecordedDate = DateTime.Now,
                ReturnPercentage = portfolioReturn
            };

            currentAccount.PortfolioReturn.Add(newReturn);

            await _accountService.Update(currentAccount.ID, currentAccount);

            return currentAccount;
        }
    }
}
