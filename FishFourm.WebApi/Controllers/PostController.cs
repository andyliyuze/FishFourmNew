﻿using Abp.WebApi.Controllers;
using FishFourm.Application.Posts;
using FishFourm.Application.Posts.Dtos;
using FishFourm.Common;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using FishFourm.WebApi.Controllers;

namespace FishFourm.Api.Controllers
{

    [RoutePrefix("api/post")]
    /// <summary>
    /// 111
    /// </summary>
    public class PostController : BaseApiController
    {

        private readonly IPostAppService _postAppService;

        /// <summary>
        /// 111
        /// </summary>
        /// <param name="postAppService"></param>
        public PostController(IPostAppService postAppService, IWebApiResponseHandle webApiResponseHandle)
            : base(webApiResponseHandle)
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
        [AuthorizeAttribute]
        public async Task<JsonResponse> CreatePost(PostInput dto)
        {
            dto.AuthorId = UserInfo.Id;
            var Id = await _postAppService.CreatePost(dto);
            return new JsonResponse(Id, 200);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("postList")]
        [HttpGet]

        public async Task<JsonResponse> PostList()
        {
            try
            {
                var posts = await _postAppService.GetAllPost();
                return new JsonResponse(posts, 200);
            }
            catch (Exception e) { throw e; }
        }

    }
}
