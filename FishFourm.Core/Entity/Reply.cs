using Abp.Domain.Entities;
using System;

namespace FishFourm.Core.Entity
{
    public class Reply : AggregateRoot<Guid>
    {
        //覆盖默认的Id
        public new Guid   Id { get; private set; }
        public Guid CommentId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreateTime { get; private set; }
        public Guid AuthorId { get; private set; }
        public bool IsDel { get; private set; }
    }
}
