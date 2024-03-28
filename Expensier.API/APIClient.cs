using Expensier.API.Models;
using Expensier.Domain.Models;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
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
                    ID = data.id,
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


        public async Task<IEnumerable<HistoricalData>> GetCryptoHistoricalData( string URI )
        {
            ObservableCollection<HistoricalData> list = new ObservableCollection<HistoricalData>();

            try
            {
                HttpResponseMessage response = await _client.GetAsync( URI );

                if ( !response.IsSuccessStatusCode )
                    return default;

                string jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse( jsonResponse );

                var historicalData = data.prices;

                foreach ( var record in historicalData )
                {
                    HistoricalData newRecord = new HistoricalData()
                    {
                        Date = DateTimeOffset.FromUnixTimeMilliseconds( (long) record[0] ).DateTime,
                        Price = (double) record[1]
                    };

                    list.Add( newRecord );
                }

                return list;
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                return default;
            }
        }
    }
}
