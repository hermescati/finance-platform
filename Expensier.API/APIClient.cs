using Expensier.API.Models;
using Expensier.Domain.Models;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using RestSharp;


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


        public async Task<Asset> FetchCryptoAsset( string URI )
        {
            try
            {
                var uri = "https://api.coingecko.com/api/v3/" + URI;
                var options = new RestClientOptions( uri );
                var client = new RestClient( options );
                var request = new RestRequest( "" );

                request.AddHeader( "accept", "application/json" );
                request.AddHeader( "x-cg-demo-api-key", "CG-jfN74xRc93dnGykXBUqND1Cv\t" );

                var response = await client.GetAsync( request );

                if ( !response.IsSuccessful || response.Content == null )
                    return default;

                dynamic data = JObject.Parse( response.Content );
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


        public async Task<IEnumerable<HistoricalData>> FetchCryptoHistoricalData( string URI )
        {
            ObservableCollection<HistoricalData> list = new ObservableCollection<HistoricalData>();

            try
            {
                var uri = "https://api.coingecko.com/api/v3/" + URI;
                var options = new RestClientOptions( uri );
                var client = new RestClient( options );
                var request = new RestRequest( "" );

                request.AddHeader( "accept", "application/json" );
                request.AddHeader( "x-cg-demo-api-key", "CG-jfN74xRc93dnGykXBUqND1Cv\t" );

                var response = await client.GetAsync( request );

                if ( !response.IsSuccessful || response.Content == null )
                    return default;

                dynamic data = JObject.Parse( response.Content );

                foreach ( var priceRecord in data.prices )
                {
                    HistoricalData newRecord = new HistoricalData()
                    {
                        Date = DateTimeOffset.FromUnixTimeMilliseconds( ( long ) priceRecord[0] ).DateTime,
                        Price = ( double ) priceRecord[1]
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


        public async Task<double> FetchCryptoHistoricalPrice( string URI )
        {
            try
            {
                var uri = "https://api.coingecko.com/api/v3/" + URI;
                var options = new RestClientOptions( uri );
                var client = new RestClient( options );
                var request = new RestRequest( "" );

                request.AddHeader( "accept", "application/json" );
                request.AddHeader( "x-cg-demo-api-key", "CG-jfN74xRc93dnGykXBUqND1Cv\t" );

                var response = await client.GetAsync( request );

                if ( !response.IsSuccessful || response.Content == null )
                    return default;

                dynamic data = JObject.Parse( response.Content );
                return data.market_data.current_price.usd;
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                return default;
            }
        }
    }
}
