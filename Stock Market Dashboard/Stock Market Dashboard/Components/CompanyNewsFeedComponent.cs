using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using Stock_Market_Dashboard.Data;
using System;
using System.Collections.Generic;

namespace Stock_Market_Dashboard.Components
{
    public partial class CompanyNewsFeedComponentBase : DataSettingsComponentBase
    {
        private List<CompanyNewsArticle> companyNewsDataAmazon;
        private List<CompanyNewsArticle> companyNewsDataApple;
        private List<CompanyNewsArticle> companyNewsDataGoogle;

        protected MarkupString companyNewsContentAmazon = new MarkupString("Content for Amazon.");
        protected MarkupString companyNewsContentApple = new MarkupString("Content for Apple.");
        protected MarkupString companyNewsContentGoogle = new MarkupString("Content for Google.");

        protected string selectedTab = "amazon";
        protected void OnSelectedTabChanged(string name)
        {
            selectedTab = name;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                DateTime startDate = DateTime.Now.AddDays(-7);
                DateTime endDate = DateTime.Now;

                companyNewsDataAmazon = service.GetStockMarketCompanyNewsAsync(amazonCodeName, startDate, endDate);
                companyNewsDataApple = service.GetStockMarketCompanyNewsAsync(appleCodeName, startDate, endDate);
                companyNewsDataGoogle = service.GetStockMarketCompanyNewsAsync(googleCodeName, startDate, endDate);

                CompanyNewsRefresh();
            }
        }

        protected void CompanyNewsRefresh()
        {
            if(amazon)
                companyNewsContentAmazon = ConstructCompanyNewsContent(amazonCodeName);
            if(apple)
                companyNewsContentApple = ConstructCompanyNewsContent(appleCodeName);
            if(google)
                companyNewsContentGoogle = ConstructCompanyNewsContent(amazonCodeName);
        }

        private MarkupString ConstructCompanyNewsContent(string companyName)
        {
            string stringBuilder = companyName == amazonCodeName ? "Content for Amazon." : (companyName == appleCodeName ? "Content for Apple." : "Content for Google.");

            List<CompanyNewsArticle> articles = companyName == amazonCodeName ? companyNewsDataAmazon : (companyName == appleCodeName ? companyNewsDataApple : companyNewsDataGoogle);

            if (articles != null)
            {
                stringBuilder = "";
                foreach (var article in articles)
                {
                    stringBuilder +=  $"Headline: {article.headline}<br/>" +
                               $"Category: {article.category}<br/>" +
                               $"Date Posted: {article.datetime}<br/>" +
                               $"id: {article.id}<br/>" +
                               $"image: {article.image}<br/>" +
                               $"Related: {article.related}<br/>" +
                               $"Source: {article.source}<br/>" +
                               $"Summary: {article.summary}<br/>" +
                               $"URL: {article.url}<br/><br/><br/>";
                }
            }

            return new MarkupString(stringBuilder);
        }
    }
}
