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
        private readonly APIClient _client;

        public CryptoService(APIClient client)
        {
            _client = client;
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
    }
}
