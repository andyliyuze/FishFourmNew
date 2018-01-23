using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Core.Entity
{
    /// <summary>
    /// 评论聚合
    /// </summary>
    public class Comment : AggregateRoot<Guid>
    { 
        public Guid PostId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreateTime { get; private set; }
        public Nullable<Guid> AuthorId { get; private set; }
        public bool IsDel { get; private set; }

        /// <summary>
        /// 子级回复
        /// </summary>
        public  IEnumerable<Comment> ChildComments { get; set; }

    }

    public class CommentDTO : AggregateRoot<Guid>
    {
        public Guid PostId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreateTime { get; private set; }
        public Nullable<Guid> AuthorId { get; private set; }
        public bool IsDel { get; private set; }

        /// <summary>
        /// 父级回复
        /// </summary>
        public Guid ParentCommentId { get; private set; }

    }
}

