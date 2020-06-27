using Microsoft.AspNetCore.Components;

namespace Stock_Market_Dashboard.Components
{
    public partial class CompanyNewsFeedComponentBase : ComponentBase
    {
        protected string selectedTab = "Amazon";
        protected void OnSelectedTabChanged(string name)
        {
            selectedTab = name;
        }
    }
}
