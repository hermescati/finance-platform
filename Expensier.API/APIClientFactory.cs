using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.API
{
    public class APIClientFactory
    {
        private readonly string _apiKey;

        public APIClientFactory(string apiKey)
        {
            _apiKey = apiKey;
        }

        public APIClient CreateAPIClient()
        {
            return new APIClient(_apiKey);
        }
    }
}
