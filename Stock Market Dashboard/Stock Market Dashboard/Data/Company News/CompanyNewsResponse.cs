namespace Stock_Market_Dashboard.Data
{
    public class CompanyNewsResponse
    {        
        public string category { get; set; }    // News category

        public int datetime { get; set; }       // Published time in UNIX timestamp

        public string headline { get; set; }    // News headline

        public int id { get; set; }             // News ID.This value can be used for minId params to get the latest news only

        public string image { get; set; }       // Thumbnail image URL

        public string related { get; set; }     // Related stocks and companies mentioned in the article

        public string source { get; set; }      // News source
        public string summary { get; set; }     // News summary
        public string url { get; set; }         // URL of the original article
    }
}