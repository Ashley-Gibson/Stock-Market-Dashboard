using Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stock_Market_Dashboard.Data
{
    public class StockMarketServices
    {
        private string APIKey => "bqdkitnrh5re4rmf4580";
        private string StockWebhook => "https://finnhub.io/api/v1/stock/candle";
        private string CompanyNewsWebhook => "https://finnhub.io/api/v1/company-news";

        private readonly HttpClient client = new HttpClient();

        public StockMarketServices()
        {
            client.Timeout = TimeSpan.FromSeconds(10);
        }

        private string BuildStockWebhookPath(string companyCode, DateTime startDate, DateTime endDate) 
        {
            long startUnixTimestamp = Conversions.ConvertDateToUnix(startDate);
            long endUnixTimestamp = Conversions.ConvertDateToUnix(endDate);
            
            return StockWebhook + "?symbol=" + companyCode + "&from=" + startUnixTimestamp + "&to=" + endUnixTimestamp + "&resolution=D&token=" + APIKey;
        }

        public async Task<CompanyNewsResponse> GetStockMarketDataForCompanyAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            string path = BuildStockWebhookPath(companyCode, startDate, endDate);
            CompanyNewsResponse response = await client.GetJsonAsync<CompanyNewsResponse>(path).ConfigureAwait(continueOnCapturedContext:false);
            return response;
        }

        public string BuildCompanyNewsWebhookPath(string companyCode, DateTime startDate, DateTime endDate)
        {
            long startUnixTimestamp = Conversions.ConvertDateToUnix(startDate);
            long endUnixTimestamp = Conversions.ConvertDateToUnix(endDate);

            return CompanyNewsWebhook + "?symbol=" + companyCode + "&from=" + startUnixTimestamp + "&to=" + endUnixTimestamp + "&resolution=D&token=" + APIKey;
        }

        public async Task<CompanyNewsResponse> GetStockMarkeyCompanyNewsAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            string path = BuildCompanyNewsWebhookPath(companyCode, startDate, endDate);
            CompanyNewsResponse response = await client.GetJsonAsync<CompanyNewsResponse>(path).ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
    }
}
