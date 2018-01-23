using Abp.EntityFramework;
using AutoMapper;
using FishFourm.Core.Entity;
using FishFourm.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.EntityFramework.EntityFramework.Repository
{
   public class CommentRepository : FishFourmRepositoryBase<CommentDTO, Guid> 
    {
        public CommentRepository(IDbContextProvider<FishFourmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public IEnumerable<Comment> GetComment(Guid postId)
        {
            IEnumerable<Comment> comments = new List<Comment>();
            var commentDTOs = this.Context.Comment.Where(a => a.PostId == postId ).ToList();
            foreach (var parent in commentDTOs)
            {
                var comment =  Mapper.Map<Comment>(comments);
                var childComments = this.Context.Comment.Where(a => a.ParentCommentId == parent.Id).ToList();
                comment.ChildComments = Mapper.Map<IEnumerable<Comment>>(childComments);
                comments.
            }
        }

    }
}
