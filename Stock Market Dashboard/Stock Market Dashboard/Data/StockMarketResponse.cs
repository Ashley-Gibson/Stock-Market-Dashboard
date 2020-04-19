namespace Stock_Market_Dashboard.Data
{
    public class StockMarketResponse
    {        
        public double o { get; set; } // Open price for the day
        public double h { get; set; } // High price for the day
        public double l { get; set; } // Low price for the day
        public double c { get; set; } // Current price
        public double pc { get; set; } // Previous close price
    }
}