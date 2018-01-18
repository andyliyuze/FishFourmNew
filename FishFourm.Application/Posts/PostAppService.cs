using System;
using FishFourm.Core.Repository;
using Abp.AutoMapper;
using FishFourm.Application.Posts.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using AutoMapper;
using FishFourm.Core.Entity;

using System.Linq;
using Abp.Authorization;

namespace FishFourm.Application.Posts
{
    public class PostAppService : ApplicationService, IPostAppService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostAppService(IPostRepository postRepository,
            IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<IList<PostDto>> GetAllPost()
        {
            var posts = await _postRepository.GetAllListAsync();
            var users = _userRepository.GetAll();
            var dtos = from p in posts
                       join u in users on p.AuthorId equals u.Id
                       select new PostDto
                       {
                           Id = p.Id,
                           AlterTime = p.AlterTime,
                           AuthorId = p.AuthorId,
                           Content = p.Content,
                           CreateTime = p.CreateTime,
                           Title = p.Title,
                           AuthorName = u.UserName
                       };
         dtos =  dtos.Where(a => a.IsDel == false).ToList();
            var postsdto = posts.Join(users, a => a.AuthorId, b => b.Id,
                (a, b) => new PostDto
                {
                    Id = a.Id,
                    AlterTime = a.AlterTime,
                    AuthorId = a.AuthorId,
                    AuthorName = b.UserName,
                    Content = a.Content,
                    CreateTime = a.CreateTime,
                    Title = a.Title
                }).ToList();
            return dtos.ToList();
        }

        

        public async Task<PostDto> ReadPost(Guid postId)
        {
            var post = await _postRepository.GetAsync(postId);

            if (post == null)
            {
                return null;
            }
    
            var user = await _userRepository.GetAsync(post.AuthorId);
            
            //两次Map才能拿到完整的postDto
            var postDto = post.MapTo<PostDto>();
            Mapper.Map(user, postDto);
            return postDto;
        }

        public async Task<bool> CreatePost(CreatePostDto postDto)
        {
            var post = new Post(postDto.AuthorId, postDto.Title, postDto.Content);
            var postResult = await _postRepository.InsertAsync(post);
            return postResult != null;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByAuthorId(Guid authorId)
        {
            var posts = await _postRepository.GetPostsByAuthorId(authorId);
            var postDtos = posts.MapTo<IEnumerable<PostDto>>();
            var user = await _userRepository.GetAsync(authorId);
            foreach (var item in postDtos)
            {
                item.AuthorName = user.UserName;
            }

            return postDtos;
        }

        public async Task<bool> UpdatePost(PostDto postDto)
        {
            var post = await _postRepository.GetAsync(postDto.Id);
            post.Update(postDto.Title, postDto.Content);
            var updatePost = await _postRepository.UpdateAsync(post);
            return updatePost != null;
        }
    }
}
