using Expensier.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.API
{
    public class APIClient : HttpClient
    {
        private readonly string _apiKey;
        public APIClient(string apiKey)
        {
            this.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
            _apiKey = apiKey;
        }

        public async Task<T> DeserializeResponse<T>(string uri)
        {
            using var response = await GetAsync($"{uri}?apikey={_apiKey}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(responseStream);
                using var jsonResponse = new JsonTextReader(streamReader);

                JsonSerializer serializer = new JsonSerializer();

                try
                {
                    return serializer.Deserialize<List<T>>(jsonResponse)[0];
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Invalid JSON!");
                }
            }
            else
            {
                Console.WriteLine("HTTP Response cannot be deserialized");
            }
            return default(T);
        }
    }
}
