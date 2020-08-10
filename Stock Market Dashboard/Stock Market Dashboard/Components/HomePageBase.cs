using Microsoft.AspNetCore.Components;
using Stock_Market_Dashboard.Data;
using System.Threading.Tasks;

namespace Stock_Market_Dashboard.Components
{
    public class HomePageBase : ComponentBase
    {
        protected bool amazon = false;
        protected bool apple = true;
        protected bool google = false;

        protected const string amazonCodeName = "AMZN";
        protected const string appleCodeName = "AAPL";
        protected const string googleCodeName = "GOOGL";

        protected readonly StockMarketServices service = new StockMarketServices();
    }
}
