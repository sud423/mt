using System.Text.Json.Serialization;

namespace Mt.Ask.Web.Models
{
    public class ExternalLogin
    {
        public string OpenId { get; set; }

        [JsonIgnore]
        public int WebSiteId { get; set; }


    }
}
