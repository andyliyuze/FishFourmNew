using Abp.WebApi.Controllers;
using FishFourm.Application;
using FishFourm.Application.Posts;
using FishFourm.Application.Posts.Dtos;
using System;
using System.Web.Http;

namespace FishFourm.Api.Controllers
{
    public class PostController : AbpApiController
    {

        private readonly IPostAppService _postAppService;

        public PostController(IPostAppService  postAppService)
        {
            _postAppService = postAppService;
        }


        [HttpGet]
        public PostDto ReadPost(Guid postId)
        {
            return null;
        }

    }
}
