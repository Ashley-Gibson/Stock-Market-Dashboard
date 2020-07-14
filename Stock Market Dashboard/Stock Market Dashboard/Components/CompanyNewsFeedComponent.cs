using Microsoft.AspNetCore.Components;

namespace Stock_Market_Dashboard.Components
{
    public partial class CompanyNewsFeedComponentBase : ComponentBase
    {
        public string selectedTab = "amazon";
        public void OnSelectedTabChanged(string name)
        {
            selectedTab = name;
        }
    }
}
