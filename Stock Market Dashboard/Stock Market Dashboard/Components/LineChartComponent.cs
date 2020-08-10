using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Stock_Market_Dashboard.Data;
using Extensions;
using Blazorise.Charts;

namespace Stock_Market_Dashboard.Components
{
    public partial class LineChartComponentBase : HomePageBase
    {
        protected LineChart<DataPoint> lineChart = new LineChart<DataPoint>();

        private List<DataPoint> companyPriceDataPoints = new List<DataPoint>();

        private List<string> companies = new List<string>();

        private readonly List<string> timestamps = new List<string>();

        private StockMarketResponse stockMarketDataAmazon;
        private StockMarketResponse stockMarketDataApple;
        private StockMarketResponse stockMarketDataGoogle;

        private readonly List<string> backgroundColours = new List<string>
{
        ChartColor.FromRgba(255, 99, 132, 0.2f),
        ChartColor.FromRgba(54, 162, 235, 0.2f),
        ChartColor.FromRgba(255, 206, 86, 0.2f),
        ChartColor.FromRgba(75, 192, 192, 0.2f),
        ChartColor.FromRgba(153, 102, 255, 0.2f),
        ChartColor.FromRgba(255, 159, 64, 0.2f)
    };
        private readonly List<string> borderColours = new List<string>
{
        ChartColor.FromRgba(255, 99, 132, 1f),
        ChartColor.FromRgba(54, 162, 235, 1f),
        ChartColor.FromRgba(255, 206, 86, 1f),
        ChartColor.FromRgba(75, 192, 192, 1f),
        ChartColor.FromRgba(153, 102, 255, 1f),
        ChartColor.FromRgba(255, 159, 64, 1f)
    };

        protected async void OnAmazonChanged(bool value)
        {
            amazon = value;
            await Task.WhenAll(HandleRedraw());
            
        }
        protected async void OnAppleChanged(bool value)
        {
            apple = value;
            await Task.WhenAll(HandleRedraw());
        }
        protected async void OnGoogleChanged(bool value)
        {
            google = value;
            await Task.WhenAll(HandleRedraw());
        }

        protected struct DataPoint
        {
            public object X { get; set; }

            public object Y { get; set; }
        }

        protected object lineChartOptions = new
        {
            Title = new
            {
                Display = false,
                Text = "Stock Market Line Chart"
            },
            Scales = new
            {
                XAxes = new object[]
                {
                new {
                    ScaleLabel = new
                    {
                        Display = true, LabelString = "Date"
                    }
                }
                                },
                YAxes = new object[]
                {
                new {
                    ScaleLabel = new
                    {
                        Display = true, LabelString = "Price"
                    }
                }
                                }
            },
            Tooltips = new
            {
                Mode = "nearest",
                Intersect = false
            },
            Hover = new
            {
                Mode = "nearest",
                Intersect = false
            }
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                DateTime startDate = DateTime.Now.AddDays(-7);
                DateTime endDate = DateTime.Now;

                stockMarketDataAmazon = await service.GetStockMarketDataForCompanyAsync(amazonCodeName, startDate, endDate);
                stockMarketDataApple = await service.GetStockMarketDataForCompanyAsync(appleCodeName, startDate, endDate);
                stockMarketDataGoogle = await service.GetStockMarketDataForCompanyAsync(googleCodeName, startDate, endDate);

                GetTimestamps();

                await Task.WhenAll(HandleRedraw());
            }            
        }

        private void GetTimestamps()
        {
            foreach (var timestamp in stockMarketDataAmazon.t)
                timestamps.Add(Conversions.ConvertUnixToDate(timestamp).ToShortDateString());
        }

        protected async Task HandleRedraw()
        {
            await Task.WhenAll(lineChart.Clear());

            GetSelectedCompanies();

            int dataPointIndex = 0;
            List<LineChartDataset<DataPoint>> datasets = new List<LineChartDataset<DataPoint>>();
            foreach (string company in companies)
            {
                GetDataForCompany(company);
                datasets.Add(GetLineChartDataset(dataPointIndex));
                dataPointIndex++;
            }

            await Task.WhenAll(lineChart.AddLabelsDatasetsAndUpdate(timestamps.ToArray(), datasets.ToArray()));
        }

        private void GetSelectedCompanies()
        {
            companies = new List<string>();

            if (amazon)
                companies.Add(amazonCodeName);
            if (apple)
                companies.Add(appleCodeName);
            if (google)
                companies.Add(googleCodeName);
        }

        private void GetDataForCompany(string company)
        {
            DataPoint companyPriceDataPoint = new DataPoint();
            companyPriceDataPoints = new List<DataPoint>();

            switch (company)
            {
                case amazonCodeName:
                    for (int i = 0; i < stockMarketDataAmazon.c.Length - 1; i++)
                    {
                        companyPriceDataPoint.X = Conversions.ConvertUnixToDate((long)stockMarketDataAmazon.t[i]).ToShortDateString();
                        companyPriceDataPoint.Y = Math.Round(stockMarketDataAmazon.c[i], 2, MidpointRounding.AwayFromZero);
                        companyPriceDataPoints.Add(companyPriceDataPoint);
                    }
                    break;
                case appleCodeName:
                    for (int i = 0; i < stockMarketDataApple.c.Length - 1; i++)
                    {
                        companyPriceDataPoint.X = Conversions.ConvertUnixToDate((long)stockMarketDataApple.t[i]).ToShortDateString();
                        companyPriceDataPoint.Y = Math.Round(stockMarketDataApple.c[i], 2, MidpointRounding.AwayFromZero);
                        companyPriceDataPoints.Add(companyPriceDataPoint);
                    }
                    break;
                case googleCodeName:
                    for (int i = 0; i < stockMarketDataGoogle.c.Length - 1; i++)
                    {
                        companyPriceDataPoint.X = Conversions.ConvertUnixToDate((long)stockMarketDataGoogle.t[i]).ToShortDateString();
                        companyPriceDataPoint.Y = Math.Round(stockMarketDataGoogle.c[i], 2, MidpointRounding.AwayFromZero);
                        companyPriceDataPoints.Add(companyPriceDataPoint);
                    }
                    break;
            }
        }

        protected LineChartDataset<DataPoint> GetLineChartDataset(int dataPointIndex)
        {
            return new LineChartDataset<DataPoint>
            {
                Label = companies[dataPointIndex],
                Data = companyPriceDataPoints,
                BackgroundColor = backgroundColours[dataPointIndex],
                BorderColor = borderColours[dataPointIndex],
                Fill = false,
                PointRadius = 2,
                BorderDash = new List<int> { 1, 2, 3, 4, 5, 6, 7 }
            };
        }
    }
}
