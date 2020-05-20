using System;

namespace Mt.Edu.Api.Models
{
    public class Stu
    {
        /// <summary>
        /// 学员编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 班级编号
        /// </summary>
        public int ClaId { get; set; }

        /// <summary>
        /// 学员信息编号
        /// </summary>
        public int InfoId { get; set; }

        /// <summary>
        /// 状态 1：正常 0：删除(退学)
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 录入人编号
        /// </summary>
        public int UserId { get; set; }

        public virtual BasicInfo BasicInfo { get; set; }

        public virtual Cla Cla { get; set; }

    }
}
