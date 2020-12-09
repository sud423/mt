namespace Mt.Ask.Web.Models
{
    public class BrowseHistory
    {

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string Device { get; set; }

        public string Os { get; set; }

        public string Source { get; set; }

        public int SourceId { get; set; }

        public BrowseHistory(int sourceId, string ip, string browser, string os, string device,int userId=0)
        {
            UserId = userId;
            Ip = ip;
            Browser = browser;
            Device = device;
            Os = os;
            Source = "article";
            SourceId = sourceId;
        }
    }
}
