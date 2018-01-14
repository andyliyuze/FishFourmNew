
using Abp.Application.Services;
using FishFourm.Application.Posts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FishFourm.Application.Posts
{
    public interface IPostAppService: IApplicationService
    {
        Task<IList<PostDto>> GetAllPost();

        Task<PostDto> ReadPost(Guid Id);

        Task<bool> CreatePost(CreatePostDto postDto);

        Task<IEnumerable<PostDto>> GetPostsByAuthorId(Guid authorId);

        Task<bool> UpdatePost(PostDto postDto);
    }
}
