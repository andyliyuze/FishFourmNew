using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Core.Entity
{
    /// <summary>
    /// 评论聚合,因为评论有唯一标识，和有独立边界
    /// </summary>
    public class Comment : AggregateRoot<Guid>
    {

        public Comment() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId">帖子id</param>
        /// <param name="authorId">作者id</param>
        /// <param name="content">内容</param>
        public Comment(Guid postId, Guid authorId, string content)
        {
            if (postId == Guid.Empty || authorId == Guid.Empty || string.IsNullOrEmpty(content))
            {
                throw new Exception("评论构造参数不可为空");
            }
            this.PostId = postId;
            this.AuthorId = authorId;
            this.Content = content;
            this.CreateTime = DateTime.Now;
            this.IsDel = false;
            this.Id = Guid.NewGuid();
        }

        public Guid PostId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreateTime { get; private set; }
        public Guid AuthorId { get; private set; }
        public bool IsDel { get; private set; }
    }

   
}

