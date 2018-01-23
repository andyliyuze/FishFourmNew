using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Application.Posts.Dtos
{
    public class PostInput : EntityDto<Guid>
    {
        public Guid AuthorId { get; set; }
 
        [Description("标题不可为空")]
        [Required]
        public string Title { get; set; }

        [Description("内容不可为空")]
        [Required]
        public string Content { get; set; }
       
    }
}
