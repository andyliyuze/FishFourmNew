using FishFourm.Application.Coments.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace FishFourm.Application.Coments
{
   public interface ICommentAppService
    {
        Task<CommentOutput> AddComment(CommentInput commentInput);
     
        /// <summary>
        /// 帖子Id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<IEnumerable<CommentOutput>> GetComments(Guid postId);
    }
}
