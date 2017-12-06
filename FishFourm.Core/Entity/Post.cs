using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;

namespace FishFourm.Core.Entity
{
    public class Post  : AggregateRoot<Guid>
    {
      
      
        public   Guid AuthorId { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public DateTime CreateTime { get; private set; }

        public Nullable<DateTime> AlterTime { get; private set; }

        public bool IsDel { get; private set; }

        public Post() { }

         
        public Post(Guid authorId, string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title)) { throw new ApplicationException("帖子题目不能为空"); }
            if (string.IsNullOrWhiteSpace(content)) { throw new ApplicationException("帖子内容不能为空"); }
            if (authorId==Guid.Empty) { throw new ApplicationException("帖子作者不能为空"); }
            Id = Guid.NewGuid();
        
            AuthorId = authorId;
            Title = title;
            Content = content;
            IsDel = false;
            CreateTime = DateTime.Now;
        }

        public void Update(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title)) { throw new Exception("帖子题目不能为空"); }
            if (string.IsNullOrWhiteSpace(content)) { throw new Exception("帖子内容不能为空"); }
            Title = title;
            Content = content;
            AlterTime = DateTime.Now;
        }

        public void SoftDelete()
        {
            IsDel = true;
        }
    }
}
