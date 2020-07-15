using Microsoft.AspNetCore.Components;
using Stock_Market_Dashboard.Data;
using System;
using System.Threading.Tasks;

namespace Stock_Market_Dashboard.Components
{
    public partial class CompanyNewsFeedComponentBase : ComponentBase
    {
        private readonly StockMarketServices service = new StockMarketServices();

        public string selectedTab = "amazon";
        public void OnSelectedTabChanged(string name)
        {
            selectedTab = name;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                DateTime startDate = DateTime.Now.AddDays(-7);
                DateTime endDate = DateTime.Now;

                companyNewsDataAmazon = await service.GetStockMarketDataForCompanyAsync(amazonCodeName, startDate, endDate);
                companyNewsDataApple = await service.GetStockMarketDataForCompanyAsync(appleCodeName, startDate, endDate);
                companyNewsDataGoogle = await service.GetStockMarketDataForCompanyAsync(googleCodeName, startDate, endDate);

                await Task.WhenAll(HandleRedraw());
            }
        }
    }
}
