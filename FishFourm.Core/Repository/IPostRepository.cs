using Abp.Domain.Repositories;
using FishFourm.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Core.Repository
{
    //IRepository定义了很多通用接口，因此不需要重复定义
    public interface IPostRepository:IRepository<Post, Guid>
    {
        Task<IEnumerable<Post>> GetPostsByAuthorId(Guid Id);

    }
}
