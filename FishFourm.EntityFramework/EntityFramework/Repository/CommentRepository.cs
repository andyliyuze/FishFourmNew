using Abp.EntityFramework;
using AutoMapper;
using FishFourm.Core.Entity;
using FishFourm.Core.Repository;
using FishFourm.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.EntityFramework.EntityFramework.Repository
{
    public class CommentRepository : FishFourmRepositoryBase<Comment, Guid>, ICommentRepository
    {
        public CommentRepository(IDbContextProvider<FishFourmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            return await this.InsertAsync(comment);
        }

        public async Task<IEnumerable<Comment>> GetComments(Guid postId)
        {
            var comments = await this.GetAllListAsync(a => a.PostId == postId);
            return comments;
        }

    }
}
