
using Abp.Application.Services;
using FishFourm.Application.Posts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FishFourm.Application.Posts
{
    public interface IPostAppService: IApplicationService
    {
        Task<IList<PostOutput>> GetAllPost();

        Task<PostOutput> ReadPost(Guid Id);

        Task<PostOutput> CreatePost(PostInput postDto);

        Task<IEnumerable<PostOutput>> GetPostsByAuthorId(Guid authorId);

        Task<PostOutput> UpdatePost(PostInput postDto);

        Task<Guid> DeletePost(Guid Id);
    }
}
