using Abp.EntityFramework;
using FishFourm.Core.Entity;
using FishFourm.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.EntityFramework.Repository
{
    /// <summary>
    /// 实现帖子的仓储，通过继承了RepositoryBase基类，省去大量实现的代码
    /// </summary>
    public class PostRepository : FishFourmRepositoryBase<Post, Guid>, IPostRepository
    {
        public PostRepository(IDbContextProvider<FishFourmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthorId(Guid authorId)
        {
            var post = await Context.Post.Where(a => a.AuthorId == authorId && a.IsDel == false).ToListAsync();
           await InsertAndGetIdAsync(post.FirstOrDefault());
            return post;
        }



    }
}
