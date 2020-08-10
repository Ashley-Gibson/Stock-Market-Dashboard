namespace Stock_Market_Dashboard.Components
{
    public partial class DataSettingsComponentBase : HomePageBase
    {
        protected void OnAmazonChanged(bool value)
        {
            amazon = value;
            CompanyNewsRefresh();

        }
        protected void OnAppleChanged(bool value)
        {
            apple = value;
            CompanyNewsRefresh();
        }
        protected void OnGoogleChanged(bool value)
        {
            google = value;
            CompanyNewsRefresh();
        }
    }
}
