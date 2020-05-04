namespace Stock_Market_Dashboard.Data
{
    public class StockMarketResponse
    {        
        public double[] o { get; set; } // List of open prices for returned candles
        public double[] h { get; set; } // List of high prices for returned candles
        public double[] l { get; set; } // List of low prices for returned candles
        public double[] c { get; set; } // List of close prices for returned candles
        public double[] v { get; set; } // List of volume data for returned candles
        public long[] t { get; set; } // List of timestamps for returned candles
        public string s { get; set; } // Status of the response. This field can either be ok or no_data.
    }
}