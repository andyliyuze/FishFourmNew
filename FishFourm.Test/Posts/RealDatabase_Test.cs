using FishFourm.EntityFramework;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using EntityFramework;
using System.Data.Entity;

namespace FishFourm.Test.Posts
{
    public class RealDatabase_Test
    {
        [Fact]
        public async Task Post_Author_ShouldNotBeNull()
        {
            FishFourmDbContext dbContext = new FishFourmDbContext();
            var post = await dbContext.Post.FirstOrDefaultAsync();
             
        }
    }
}
