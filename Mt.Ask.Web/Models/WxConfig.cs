using System.Collections.Generic;

namespace Mt.Ask.Web.Models
{
    public class WxConfig
    {
        public string AppId { get; set; }

        public long Timestamp { get; set; }

        public string NonceStr { get; set; }

        public string Signature { get; set; }

        public IEnumerable<string> JsApiList { get; set; }
    }
}
