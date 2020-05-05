using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stock_Market_Dashboard.Data
{
    public class StockMarketService
    {
        private string APIKey => "bqdkitnrh5re4rmf4580";
        private string Webhook => "https://finnhub.io/api/v1/stock/candle";

        private readonly HttpClient client = new HttpClient();

        public StockMarketService()
        {
            client.Timeout = TimeSpan.FromSeconds(10);
        }

        private string BuildWebhookPath(string companyCode, DateTime startDate, DateTime endDate) 
        {
            long startUnixTimestamp = ConvertDateToUnix(startDate);
            long endUnixTimestamp = ConvertDateToUnix(endDate);
            
            return Webhook + "?symbol=" + companyCode + "&from=" + startUnixTimestamp + "&to=" + endUnixTimestamp + "&resolution=D&token=" + APIKey;
        }

        private long ConvertDateToUnix(DateTime date)
        {
            TimeSpan unixDate = date - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc); 

            return (long)unixDate.TotalSeconds;
        }

        public DateTime ConvertUnixToDate(long unixTimestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(unixTimestamp);
        }

        public async Task<StockMarketResponse> GetStockMarketDataForCompanyAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            string path = BuildWebhookPath(companyCode, startDate, endDate);
            StockMarketResponse response = await client.GetJsonAsync<StockMarketResponse>(path).ConfigureAwait(continueOnCapturedContext:false);
            return response;
        }
    }
}
