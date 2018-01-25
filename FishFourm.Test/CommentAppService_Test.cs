using FishFourm.Application.Coments;
using FishFourm.Application.Coments.DTO;
using FishFourm.Core.Entity;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FishFourm.Test
{
    public class CommentAppService_Test : FishFourmTestBase
    {
        private ICommentAppService _commentAppService;
        public CommentAppService_Test()
        {
            _commentAppService = LocalIocManager.Resolve<ICommentAppService>();
        }
     
        [Fact]
        public async void AddComment_Test_Should_Success()
        {     
            var postId = this.UsingDbContext<Post>(a => a.Post.FirstOrDefault()).Id;
            var authorId = this.UsingDbContext<User>(a => a.User.FirstOrDefault()).Id;
            var commentInput = new CommentInput()
            {
                Content = "新的评论",
                AuthorId = authorId,
                PostId = postId,
            };
            var output = await _commentAppService.AddComment(commentInput);
            output.ShouldNotBeNull();
            output.AuthorName.ShouldNotBeNull();

            this.Dispose();
        }

        [Fact]
        public async void GetComments_Test_ShouldBeNotNull()
        {
            InitComment();
            var postId = this.UsingDbContext<Post>(a => a.Post.FirstOrDefault()).Id;
            var comments = await _commentAppService.GetComments(postId);
            Assert.Equal(4, comments.Count());

            this.Dispose();
        }
        void InitComment()
        {
            var postId = this.UsingDbContext<Post>(a => a.Post.FirstOrDefault()).Id;
            var authorId = this.UsingDbContext<User>(a => a.User.FirstOrDefault()).Id;
            List<Comment> comments = new List<Comment>()
            {
                new Comment(postId,authorId,"Comment1"),
                new Comment(postId,authorId,"Comment12"),
                new Comment(postId,authorId,"Comment13"),
                new Comment(postId,authorId,"Comment14")
            };
            this.UsingDbContext(a => a.Comment.
            AddOrUpdate(comments[0], comments[1], comments[2], comments[3]));
        }
    }
}
