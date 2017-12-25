using FishFourm.Application.Posts;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using FishFourm.Application;
using Castle.MicroKernel.Registration;
using FishFourm.Core.Entity;
using FishFourm.Application.Posts.Dtos;
using NSubstitute;
using AutoMapper;
using FishFourm.Core.Repository;
using FishFourm.EntityFramework.Repository;
using System;
using System.Linq.Expressions;
namespace FishFourm.Test.Posts
{
    public class PostAppService_Tests : FishFourmTestBase
    {
        private   IPostAppService _postAppService;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IPostRepository _postRepositoryMock;

        public PostAppService_Tests()
        {
             _postRepositoryMock = Substitute.For<IPostRepository>();
             _userRepositoryMock = Substitute.For<IUserRepository>();
             _postAppService = new PostAppService(_postRepositoryMock, _userRepositoryMock);    
        }

        [Fact]
        public async Task Should_Get_All_Post()
        {
            var posts = await _postAppService.GetAllPost();
            posts.Count().ShouldBe(4);
        }

        //不依赖于外部接口的单元测试
        [Fact]
        public async Task ReadPost_Test()
        {
            //Arrange
            var post = UsingDbContext<Post>(a => a.Post.FirstOrDefault());
            var Id = post.Id;
          
            var _postRepositoryMock = Substitute.For<IPostRepository>();
            var _userRepositoryMock = Substitute.For<IUserRepository>();
            var postAppServiceMock = new PostAppService(_postRepositoryMock, _userRepositoryMock);
            _postRepositoryMock.GetAsync(Id).Returns(Task.FromResult(post));

            //_postRepositoryMock.Single(Arg.Any<Expression<Func<Post, bool>>>()).Returns(post);


            _postRepositoryMock.Single(Arg.Is<Expression<Func<Post, bool>>>(a => a.Compile()(post))).Returns(post);

            //此时的_userRepositoryMock是桩对象，用来使方法运行通过
            _userRepositoryMock.GetAsync(post.AuthorId).Returns(Task.FromResult(new User("li")));

            //Act
            var postDto = await postAppServiceMock.ReadPost(Id);
            
            //Assert       
            await _postRepositoryMock.Received().GetAsync(Id);
            postDto.ShouldNotBeNull();
            postDto.AuthorName.ShouldNotBeNullOrEmpty();
           
        }

        //测试交互
        [Fact]
        public async Task CreatePost_Test()
        {
            CreatePostDto postDto = new CreatePostDto()
            {
                AuthorId = UsingDbContext<User>(a => a.User.FirstOrDefault()).Id,
                Content = "HI",
                Title = "title5"
            };
            var post = new Post(postDto.AuthorId, postDto.Title, postDto.Content);


            //此时的_postRepositoryMock是模拟对象，用来断言
            //只要InsertAsync方法接收到这些参数值就返回post实例
            _postRepositoryMock.InsertAsync(Arg.Is<Post>(
            a => a.Content == post.Content
            && a.AuthorId == post.AuthorId
            && a.Title == post.Title)).Returns(post);


           
           await  _postAppService.Received().CreatePost(postDto);

            //Act
            var flag = await _postAppService.CreatePost(postDto);


            //Assert
            //只要InsertAsync方法被调用，而且接收到参数值符合预期就断言通过
            await _postRepositoryMock.Received().InsertAsync(Arg.Is<Post>( 
            a=>a.Content==post.Content
            &&a.AuthorId==post.AuthorId
            &&a.Title==post.Title));
            
        }

        //测试结果
        [Fact]
        public async Task CreatePost_Test_Count_Should_Increment()
        {
            //Arrange
             _postAppService = LocalIocManager.Resolve<IPostAppService>();
            var countBefore = UsingDbContext(a => a.Post.Count());
            CreatePostDto postDto = new CreatePostDto()
            {
                AuthorId = UsingDbContext<User>(a => a.User.FirstOrDefault()).Id,
                Content = "HI",
                Title = "title5"
            };
          
            //Act
            var flag = await _postAppService.CreatePost(postDto);
            var count = UsingDbContext(a => a.Post.Count());

            //Assert       
            Assert.True(flag);
            Assert.Equal(countBefore + 1, count);
        }


        [Fact]
        public async Task GetPostsByAuthorId_Test()
        {
            var authorId = UsingDbContext<Post>(a => a.Post.FirstOrDefault()).AuthorId;
             var postDtos=   await  _postAppService.GetPostsByAuthorId(authorId);

        }
    }
}

