using System;

namespace FishFourm.Common
{
    public class PostInput
    {     
            public Guid AuthorId { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }
    }
}
