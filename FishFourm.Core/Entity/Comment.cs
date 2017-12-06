using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Core.Entity
{
    public class Comment : AggregateRoot<Guid>
    {
        //覆盖默认的Id
        public new Guid Id { get; private set; }
        public Guid PostId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreateTime { get; private set; }
        public Nullable<Guid> AuthorId { get; private set; }
        public bool IsDel { get; private set; }


    }
}

