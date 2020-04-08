namespace Mt.Fruit.Web.Models
{
    public class BrowseHistory
    {
        public int TenantId { get; set; }

        public int WebSiteId { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string Device { get; set; }

        public string Os { get; set; }

        public string Source { get; set; }

        public int SourceId { get; set; }

        public BrowseHistory(int sourceId, string ip, string browser, string os, string device,string source, int userId = 0)
        {
            WebSiteId = 1;
            TenantId = 2;
            UserId = userId;
            Ip = ip;
            Browser = browser;
            Device = device;
            Os = os;
            Source = source;
            SourceId = sourceId;
        }
    }
}
