using Expensier.Domain.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expensier.Domain.Exceptions;
using Newtonsoft.Json;
using Expensier.Domain.Models;
using System.Collections;
using Expensier.API.Models;
using Expensier.Domain.Services.Portfolio;

namespace Expensier.API.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly APIClient _client;
        private readonly IDataService<Account> _accountService;
        private readonly IDataService<AssetTransaction> _cryptoService;

        public CryptoService(APIClient client, IDataService<Account> accountService, IDataService<AssetTransaction> cryptoService)
        {
            _client = client;
            _accountService = accountService;
            _cryptoService = cryptoService;
        }

        /// <summary>
        /// API call to retrieve market data for the provided crypto asset.
        /// </summary>
        /// <param name="symbol">The symbol of the crypto asset (symbolUSD)</param>
        /// <returns>The crypto asset with market data</returns>
        /// <exception cref="InvalidSymbolException">Thrown if the provided symbol does not belong to any crypto asset.</exception>
        public async Task<Asset> GetCrypto(string symbol)
        {
            var uri = "quote/" + symbol;
            Asset crypto = await _client.DeserializeResponse<Asset>(uri);

            if (crypto == null || crypto.CurrentPrice == 0) {
                throw new InvalidSymbolException(symbol);
            }

            return crypto;
        }

        /// <summary>
        /// API call to retrieve historical prices of the provided crypto asset.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        /// <exception cref="InvalidSymbolException"></exception>
        public async Task<IEnumerable<PriceData>> GetHistoricalPrices(string symbol)
        {
            var uri = "historical-chart/1hour/" + symbol;
            var cryptoPrices = await _client.DeserializeHistoricalPrices(uri);

            foreach (PriceData crypto in cryptoPrices)
            {
                if (crypto.Close == 0)
                {
                    throw new InvalidSymbolException(symbol);
                }
            }
            
            return cryptoPrices.Where(price => price.Date.Date == DateTime.Now.Date);
        }

        public async Task<Account> AddCrypto(Account currentAccount, Asset currentCrypto, double purchasePrice, double amount)
        {
            string symbol = currentCrypto.Symbol;

            AssetTransaction cryptoInList = currentAccount.AssetList
                .FirstOrDefault((asset) => asset.Asset.Symbol == symbol);

            if (cryptoInList == null)
            {
                AssetTransaction newCryptoAsset = new AssetTransaction()
                {
                    User = currentAccount,
                    Asset = currentCrypto,
                    PurchasePrice = purchasePrice,
                    QuantityOwned = amount
                };

                _cryptoService.GetByID(newCryptoAsset.ID);

                currentAccount.AssetList.Add(newCryptoAsset);

                await _accountService.Update(currentAccount.ID, currentAccount);
            }

            return currentAccount;
        }

        public async Task<Account> DeleteCrypto(Account currentAccount, Guid cryptoID)
        {
            currentAccount.AssetList
                .Remove(currentAccount.AssetList
                .FirstOrDefault((crypto) => crypto.ID == cryptoID));

            await _accountService.Update(currentAccount.ID, currentAccount);

            await _cryptoService.Delete(cryptoID);

            return currentAccount;
        }

        public async Task<double> GetCryptoReturns(AssetTransaction currentCrypto)
        {
            double oldPrice = (double)currentCrypto.Asset.CurrentPrice;

            Asset updatedCrypto = await GetCrypto(currentCrypto.Asset.Symbol);
            currentCrypto.Asset = updatedCrypto;

            double newPrice = (double)currentCrypto.Asset.CurrentPrice;

            return (newPrice / oldPrice) - 1;
        }

        public double GetMarketValue(double? price, double coins)
        {
            double cryptoPrice = (double)price;
            double cryptoCoins = coins;

            return cryptoPrice * cryptoCoins;
        }
    }
}
