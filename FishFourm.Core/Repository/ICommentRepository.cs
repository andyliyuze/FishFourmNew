using Abp.Domain.Repositories;
using FishFourm.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Core.Repository
{
    public interface ICommentRepository : IRepository<Comment, Guid>
    {
        Task<IEnumerable<Comment>> GetComments(Guid postId);
        Task<Comment> AddComment(Comment comment);
    }
}
