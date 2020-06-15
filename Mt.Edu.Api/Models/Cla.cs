using Csp.EF;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Mt.Edu.Api.Models
{
    /// <summary>
    /// 班级或项目名称
    /// </summary>
    public class Cla : Entity
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
        /// 项目名称/班级名称
        /// </summary>
        [Required(ErrorMessage ="名称不能为空")]
        [StringLength(100,ErrorMessage = "名称最大为100个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 渠道 training：企业培训 basic：基础教育
        /// </summary>
        [Required(ErrorMessage = "渠道不能为空")]
        [StringLength(20, ErrorMessage = "渠道最大为20个字符")]
        public string Channel { get; set; }

        /// <summary>
        /// 所属父节点编号
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 状态 1：正常 0：删除
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(1000, ErrorMessage = "简介最大为1000个字符")]
        public string Summary { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255, ErrorMessage = "备注最大为255个字符")]
        public string Remark { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual Stu Stu { get; set; }
    }
}
