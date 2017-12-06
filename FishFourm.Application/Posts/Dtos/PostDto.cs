using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FishFourm.Core.Entity;
using System;

namespace FishFourm.Application.Posts.Dtos
{
    
   public class PostDto : EntityDto<Guid>
    {
        public string AuthorName { get; set; }

        
        public Guid AuthorId { get;   set; }

        public string Title { get;   set; }

        public string Content { get;   set; }

        public DateTime CreateTime { get;   set; }

        public Nullable<DateTime> AlterTime { get;   set; }

        public bool IsDel { get;   set; }
    }
}
