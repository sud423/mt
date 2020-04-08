using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mt.Fruit.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string Type { get; set; }

        [Required(ErrorMessage ="名称不能为空")]
        [StringLength(2000, ErrorMessage = "名称最大为60个字符")]
        public string Name { get; set; }

        [JsonIgnore]
        public string Code { get; set; }

        [StringLength(2000,ErrorMessage ="描述最大为2000个字符")]
        public string Descript { get; set; }

        [JsonIgnore]
        public string SmallPic { get; set; }

        [JsonIgnore]
        public string BigPic { get; set; }

        public int Fllows { get; set; }

        public byte Status { get; set; }
    }
}
