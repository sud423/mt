using Csp.EF;
using System.ComponentModel.DataAnnotations;

namespace Mt.Edu.Api.Models
{
    public class BasicInfo : Entity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(50, ErrorMessage = "姓名最大为50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 常用手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 常用邮箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 来源 1自有 10业务员开拓 20来公司咨询 30来电咨询 40活动讲座 50网上广告推广 60线下广告推广
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 状态 1：正常 0：删除
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 渠道 training：企业培训 basic：基础教育
        /// </summary>
        [Required(ErrorMessage = "渠道不能为空")]
        [StringLength(20, ErrorMessage = "渠道最大为20个字符")]
        public string Channel { get; set; }

        /// <summary>
        /// 所在单位
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Fti { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 管理人
        /// </summary>
        public int UserId { get; set; }

        public virtual Stu Stu { get; set; }
    }
}
