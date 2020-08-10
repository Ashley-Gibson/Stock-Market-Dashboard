using Extensions;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task<StockMarketResponse> GetStockMarketDataForCompanyAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            string path = BuildStockWebhookPath(companyCode, startDate, endDate);
            StockMarketResponse response = await client.GetJsonAsync<StockMarketResponse>(path).ConfigureAwait(continueOnCapturedContext:false);
            return response;
        }

        public string BuildCompanyNewsWebhookPath(string companyCode, DateTime startDate, DateTime endDate)
        {
            //long startUnixTimestamp = Conversions.ConvertDateToUnix(startDate);
            //long endUnixTimestamp = Conversions.ConvertDateToUnix(endDate);
            string startUnixTimestamp = "2020-07-10";
            string endUnixTimestamp = "2020-07-20";

            return CompanyNewsWebhook + "?symbol=" + companyCode + "&from=" + startUnixTimestamp + "&to=" + endUnixTimestamp + "&token=" + APIKey;
        }

        public List<CompanyNewsArticle> GetStockMarketCompanyNewsAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            List<CompanyNewsArticle> articles = new List<CompanyNewsArticle>();
            string path = BuildCompanyNewsWebhookPath(companyCode, startDate, endDate);
            var task = client.GetAsync(path).ContinueWith((taskwithresponse) => {
                var response = taskwithresponse.Result;
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                articles = JsonConvert.DeserializeObject<List<CompanyNewsArticle>>(jsonString.Result);
            });
            task.Wait();
            return articles;
        }
    }
}
