using Expensier.API.Models;
using Expensier.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;


namespace Expensier.API
{
    public class APIClient : HttpClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;


        public APIClient( HttpClient client, APIKey apiKey )
        {
            _client = client;
            _apiKey = apiKey.Key;
        }


        public async Task<Asset> GetCryptoAsset( string URI )
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync( URI );

                if ( !response.IsSuccessStatusCode )
                    return default;

                string jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse( jsonResponse );

                Asset newAsset = new Asset()
                {
                    Symbol = data.symbol,
                    Name = data.name,
                    CurrentPrice = data.market_data.current_price.usd,
                    PercentageChange = data.market_data.price_change_percentage_24h,
                    Image = data.image.large
                };

                return newAsset;
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                return default;
            }
        }


        public async Task<IEnumerable<PriceData>> DeserializeHistoricalPrices( string uri )
        {
            try
            {
                using var response = await _client.GetAsync( $"{uri}?apikey={_apiKey}", HttpCompletionOption.ResponseHeadersRead );
                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader( responseStream );
                using var jsonResponse = new JsonTextReader( streamReader );

                JsonSerializer serializer = new JsonSerializer();

                try
                {
                    return serializer.Deserialize<IEnumerable<PriceData>>( jsonResponse );
                }
                catch ( JsonReaderException )
                {
                    Console.WriteLine( "Invalid JSON!" );
                    return Enumerable.Empty<PriceData>();
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"An error occurred: {ex.Message}" );
                return Enumerable.Empty<PriceData>();
            }
        }
    }
}
