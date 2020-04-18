using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stock_Market_Dashboard.Data
{
    public class StockMarketService
    {
        private string APIKey => "bqdkitnrh5re4rmf4580";
        private  string QuoteWebhook => "https://finnhub.io/api/v1/quote";

        private  HttpClient client = new HttpClient();

        public StockMarketService()
        {
            client.Timeout = TimeSpan.FromSeconds(10);
        }

        private  string BuildWebhookPath(string company = "AAPL") 
        {
            return QuoteWebhook + "?symbol=" + company + "&token=" + APIKey;
        }

        public async Task<StockMarketResponse> GetStockMarketDataForCompanyAsync(string company = "AAPL")
        {
            string path = BuildWebhookPath(company);
            StockMarketResponse response = await client.GetJsonAsync<StockMarketResponse>(path).ConfigureAwait(continueOnCapturedContext:false);
            return response;
        }
    }
}
