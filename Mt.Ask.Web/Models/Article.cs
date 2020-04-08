using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mt.Ask.Web.Models
{
    public class Article
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [StringLength(100,ErrorMessage = "标题最大为100个字符")]
        [Required(ErrorMessage ="标题不能为空")]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Clicks { get; set; }

        public int Replys { get; set; }

        [JsonIgnore]
        public byte Status { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public int TenantId { get; set; }

        [JsonIgnore]
        public int WebSiteId { get; set; }

        public virtual User User { get; set; }

        public void SetId(int tenantId,int userId,string nickName,int webSiteId,int id)
        {
            Id = id;
            TenantId = tenantId;
            UserId = userId;

            WebSiteId = webSiteId;
            CategoryId = 1;
            Status = 1;

            Author = nickName;
        }
    }
}
