using FishFourm.EntityFramework;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using EntityFramework;
using System.Data.Entity;
using FishFourm.Core.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace FishFourm.Test.Posts
{
    public class RealDatabase_Test : FishFourmTestBase
    {
        [Fact]
        public async Task Post_Author_ShouldNotBeNull()
        {
            FishFourmDbContext dbContext = new FishFourmDbContext();
          
            var postId = dbContext.Post.FirstOrDefault().Id;
            var authorId = dbContext.User.FirstOrDefault().Id;
            List<Comment> comments = new List<Comment>()
            {
                new Comment(postId,authorId,"Comment1"),
                new Comment(postId,authorId,"Comment12"),
                new Comment(postId,authorId,"Comment13"),
                new Comment(postId,authorId,"Comment14")
            };
            dbContext.Comment.AddOrUpdate(comments[0], comments[1], comments[2], comments[3]);

          await  dbContext.SaveChangesAsync();

 
        }
    }
}
