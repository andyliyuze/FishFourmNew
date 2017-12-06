using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Application.Posts.Dtos
{
    public class CreatePostDto : EntityDto
    {
        public Guid AuthorId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
