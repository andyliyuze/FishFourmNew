using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Application.Coments.DTO
{
    public class CommentOutput : EntityDto<Guid>
    {
        public Guid PostId { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
