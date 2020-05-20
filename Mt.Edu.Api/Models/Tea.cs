using Csp.EF;
using System.ComponentModel.DataAnnotations;

namespace Mt.Edu.Api.Models
{
    public class Tea : Entity
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
        /// 渠道 training：企业培训 basic：基础教育
        /// </summary>
        [Required(ErrorMessage = "渠道不能为空")]
        [StringLength(20, ErrorMessage = "渠道最大为20个字符")]
        public string Channel { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(50, ErrorMessage = "姓名最大为100个字符")]
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
        /// 固定电话
        /// </summary>
        public string FixedPhone { get; set; }

        /// <summary>
        /// 常用邮箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 主讲课程
        /// </summary>
        public string Courses { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 师资简介
        /// </summary>
        public string Profiles { get; set; }

        /// <summary>
        /// 状态 1：正常 0：删除
        /// </summary>
        public bool Status { get; set; } = true;

        public string Address { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 管理人
        /// </summary>
        public int UserId { get; set; }
    }
}
