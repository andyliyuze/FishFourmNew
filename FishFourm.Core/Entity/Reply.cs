using Abp.Domain.Entities;
using Abp.Domain.Values;
using System;

namespace FishFourm.Core.Entity
{
    public class Reply : AggregateRoot<Guid>
    {
        public Guid CommentId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreateTime { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid ReceiverId { get; set; }
    }
}
