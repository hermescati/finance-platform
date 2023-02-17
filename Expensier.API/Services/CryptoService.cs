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

namespace Expensier.API.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly APIClientFactory _clientFactory;

        public CryptoService(APIClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Crypto> GetCrypto(string symbol)
        {
            using (APIClient client = _clientFactory.CreateAPIClient())
            {
                var uri = "quote/" + symbol;
                Crypto crypto = await client.DeserializeResponse<Crypto>(uri);

                if (crypto.Price == 0) {
                    throw new InvalidSymbolException(symbol);
                }

                return crypto;
            }
        }
    }
}
