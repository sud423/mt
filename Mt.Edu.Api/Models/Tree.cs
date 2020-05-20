using System.Collections.Generic;

namespace Mt.Edu.Api.Models
{
    public class Tree
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 显示的文本
        /// </summary>
        public string Lable { get; set; }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<Tree> Children { get; set; }

        public Tree(int id,string lable, int parentId)
        {
            Id = id;
            Lable = lable;
            ParentId = parentId;
            Children = new List<Tree>();

        }

        public Tree() { }
    }
}
