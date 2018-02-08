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
using Abp.Runtime.Caching;
using Castle.Core;
using FishFourm.Application.Interceptors;
using Abp.Domain.Uow;

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

        public async Task<IList<PostOutput>> GetAllPost()
        {
            var posts = await _postRepository.GetAllListAsync(a=>a.IsDel == false);
            var users = _userRepository.GetAll();
         
            var postsdto = posts.Join(users, a => a.AuthorId, b => b.Id,
                (a, b) => new PostOutput
                {
                    Id = a.Id,
                    AlterTime = a.AlterTime,
                    AuthorId = a.AuthorId,
                    AuthorName = b.UserName,
                    Content = a.Content,
                    CreateTime = a.CreateTime,
                    Title = a.Title
                }).ToList();
            return postsdto;
        }

        [Cache]
        public async Task<PostOutput> ReadPost(Guid postId)
        { 
           var post = await _postRepository.GetAsync(postId);
      
            if (post == null)
            {
                return null;
            }
    
            var user = await _userRepository.GetAsync(post.AuthorId);
            
            //两次Map才能拿到完整的postDto
            var postDto = post.MapTo<PostOutput>();
            Mapper.Map(user, postDto);
            return postDto;
        }

        public async Task<PostOutput> CreatePost(PostInput postInput)
        { 
            var post = new Post(postInput.AuthorId, postInput.Title, postInput.Content);

            var postResult = await _postRepository.InsertAsync(post);

            var user = await _userRepository.GetAsync(post.AuthorId);

            //两次Map才能拿到完整的postDto
            var postDto = postResult.MapTo<PostOutput>();
            Mapper.Map(user, postDto);

            return postDto;
        }

        public async Task<IEnumerable<PostOutput>> GetPostsByAuthorId(Guid authorId)
        {
            var posts = await _postRepository.GetPostsByAuthorId(authorId);
            var postDtos = posts.MapTo<IEnumerable<PostOutput>>();
            var user = await _userRepository.GetAsync(authorId);

            foreach (var item in postDtos)
            {
                item.AuthorName = user.UserName;
            }

            return postDtos;
        }

        [UnitOfWork]
        public async Task<PostOutput> UpdatePost(PostInput updatePostDto)
        {
            if (updatePostDto.Id == Guid.Empty)
            {
                throw new Exception("帖子Id不可以为空");
            }

            var post = await _postRepository.GetAsync(updatePostDto.Id);
            post.Update(updatePostDto.Title, updatePostDto.Content);

            var updatedPost = await _postRepository.UpdateAsync(post);
            var user = await _userRepository.GetAsync(post.AuthorId);

            var postDto = updatedPost.MapTo<PostOutput>();
            Mapper.Map(user, postDto);

            return postDto;
        }

        public async Task<Guid> DeletePost(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new Exception("帖子Id不可以为空");
            }

            var post = await _postRepository.GetAsync(Id);

            post.SoftDelete();

            return post.Id;
        }
    }
}
