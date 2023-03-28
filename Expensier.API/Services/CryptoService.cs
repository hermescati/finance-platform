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
using Expensier.Doman.Services;
using Expensier.API.Models;

namespace Expensier.API.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly APIClient _client;
        private readonly IDataService<Account> _accountService;
        private readonly IDataService<CryptoAsset> _cryptoService;

        public CryptoService(APIClient client, IDataService<Account> accountService, IDataService<CryptoAsset> cryptoService)
        {
            _client = client;
            _accountService = accountService;
            _cryptoService = cryptoService;
        }

        public async Task<Crypto> GetCrypto(string symbol)
        {
            var uri = "quote/" + symbol;
            Crypto crypto = await _client.DeserializeResponse<Crypto>(uri);

            if (crypto.Price == 0) {
                throw new InvalidSymbolException(symbol);
            }

            return crypto;
        }

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

        public async Task<Account> AddCrypto(Account currentAccount, Crypto currentCrypto, double purchasePrice, double amount)
        {
            string symbol = currentCrypto.Symbol;

            CryptoAsset cryptoInList = currentAccount.CryptoAssetList
                .FirstOrDefault((asset) => asset.Crypto.Symbol == symbol);

            if (cryptoInList == null)
            {
                CryptoAsset newCryptoAsset = new CryptoAsset()
                {
                    Account_ = currentAccount,
                    Crypto = currentCrypto,
                    Purchase_Price = purchasePrice,
                    Amount = amount
                };

                _cryptoService.GetByID(newCryptoAsset.Id);

                currentAccount.CryptoAssetList.Add(newCryptoAsset);

                await _accountService.Update(currentAccount.Id, currentAccount);
            }

            return currentAccount;
        }

        public async Task<Account> DeleteCrypto(Account currentAccount, int cryptoID)
        {
            currentAccount.CryptoAssetList
                .Remove(currentAccount.CryptoAssetList
                .FirstOrDefault((crypto) => crypto.Id == cryptoID));

            await _accountService.Update(currentAccount.Id, currentAccount);

            await _cryptoService.Delete(cryptoID);

            return currentAccount;
        }
    }
}
