﻿using Abp.WebApi.Controllers;
using FishFourm.Application.Posts;
using FishFourm.Application.Posts.Dtos;
using FishFourm.Common; 
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;

namespace FishFourm.Api.Controllers
{
    [RoutePrefix("api/post")]
    /// <summary>
    /// 111
    /// </summary>
    public class PostController : AbpApiController
    {

        private readonly IPostAppService _postAppService;

        /// <summary>
        /// 111
        /// </summary>
        /// <param name="postAppService"></param>
        public PostController(IPostAppService postAppService)
        {
            _postAppService = postAppService;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("readPost")]
        [AuthorizeAttribute]
        public async Task<JsonResponse> ReadPost(string id)
        {
            var post = await _postAppService.ReadPost(Guid.Parse(id));
            return new JsonResponse(post, 200);
        }

      
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createPost")]
        public async Task<JsonResponse> CreatePost(CreatePostDto dto)
        {
            var flag = await _postAppService.CreatePost(dto);
            return new JsonResponse(flag, 200);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("postList")]
        [HttpGet]
        [AuthorizeAttribute]
        public async Task<JsonResponse> PostList()
        {

            var c = RequestContext;

          
            var posts = await _postAppService.GetAllPost();
            return new JsonResponse(posts, 200);
        }
       
    }
}
