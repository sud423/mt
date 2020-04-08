using System;
using System.ComponentModel.DataAnnotations;

namespace Mt.Fruit.Web.Models
{
    public class Resource
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int WebSiteId { get; set; }

        public int CategoryId { get; set; }

        public string Type { get; set; }

        [StringLength(30,ErrorMessage ="标题最大为30个字符")]
        [Required(ErrorMessage ="标题不能为空")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "描述最大为500个字符")]
        public string Descript { get; set; }

        [StringLength(255, ErrorMessage = "地址最大为255个字符")]
        [Required(ErrorMessage = "请选择要上传的文件")]
        public string Src { get; set; }

        public string Author { get; set; }

        public byte Status { get; set; }

        public int Clicks { get; set; }

        public int Replys { get; set; }

        public int Likes { get; set; }

        public int Favorites { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }


        public void SetId(int tenantId, int userId, string nickName, int webSiteId, int id)
        {
            Id = id;
            TenantId = tenantId;
            UserId = userId;

            Type = CategoryId == 44 ? "video" : "image";

            WebSiteId = webSiteId;
            Author = nickName;
            Status = 1;
        }
    }
}
