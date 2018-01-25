using FishFourm.Application.Coments;
using FishFourm.Application.Coments.DTO;
using FishFourm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FishFourm.WebApi.Controllers
{
    [AuthorizeAttribute]
    [RoutePrefix("api/comment")]
    public class CommentController : BaseApiController
    {
        private readonly ICommentAppService _commentAppService;

        public CommentController(ICommentAppService commentAppService, IWebApiResponseHandle webApiResponseHandle) 
            : base(webApiResponseHandle)
        {
            _commentAppService = commentAppService;
        }

        /// <summary>
        /// 获取评论集合
        /// </summary>
        /// <param name="postId"></param>
        [Route("getComments")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> GetComments(Guid postId)
        {

            return await  _webApiResponseHandle.CreateHttpResponse<IEnumerable<CommentOutput>>(Request, () =>
               {
                   var commentsDTO = _commentAppService.GetComments(postId);
                   return commentsDTO;
               });
        }
    }
}
