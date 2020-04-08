using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mt.Ask.Web.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int TenantId { get; set; }

        public string NickName { get; set; }

        public string HeadImgUrl { get; set; }

        [JsonIgnore]
        [StringLength(11,ErrorMessage ="手机号最大为11个字符")]
        [RegularExpression(@"^0?1[3|4|5|7|8|6|9][0-9]\d{8}$", ErrorMessage = "手机号码格式不正确")]
        [Required(ErrorMessage ="手机号不能为空")]
        public string Cell { get; set; }

        [JsonIgnore]
        public byte Status { get; set; }

        //[JsonIgnore]
        public virtual ExternalLogin ExternalLogin { get; set; }

        [JsonIgnore]
        public virtual UserLogin UserLogin { get; set; }
    }
}
