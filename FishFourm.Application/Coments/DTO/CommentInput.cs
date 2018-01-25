using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Application.Coments.DTO
{
    public class CommentInput : EntityDto<Guid>
    {
        public  new Guid Id { get; private set; }

        [Required]
        public Guid PostId { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Description("内容不可为空")]
        [Required]
        public string Content { get; set; }
    }
}
