using Extensions;
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
            long startUnixTimestamp = Conversions.ConvertDateToUnix(startDate);
            long endUnixTimestamp = Conversions.ConvertDateToUnix(endDate);
            
            return Webhook + "?symbol=" + companyCode + "&from=" + startUnixTimestamp + "&to=" + endUnixTimestamp + "&resolution=D&token=" + APIKey;
        }

        public async Task<StockMarketResponse> GetStockMarketDataForCompanyAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            string path = BuildWebhookPath(companyCode, startDate, endDate);
            StockMarketResponse response = await client.GetJsonAsync<StockMarketResponse>(path).ConfigureAwait(continueOnCapturedContext:false);
            return response;
        }
    }
}
