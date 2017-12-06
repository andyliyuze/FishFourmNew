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

namespace FishFourm.Test.Posts
{
    public class PostAppService_Tests : FishFourmTestBase
    {
        private readonly IPostAppService _postAppService;

        public PostAppService_Tests()
        {
            var _postRepositoryMock = Substitute.For<IPostRepository>();
            var _userRepositoryMock = Substitute.For<IUserRepository>();
            var _postAppService = new PostAppService(_postRepositoryMock, _userRepositoryMock);
         
            
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
            _userRepositoryMock.GetAsync(post.AuthorId).Returns(Task.FromResult(new User("li")));

            //Act
            var postDto = await postAppServiceMock.ReadPost(Id);
            
            //Assert       
            await _postRepositoryMock.Received().GetAsync(Id);
            postDto.ShouldNotBeNull();
            postDto.AuthorName.ShouldNotBeNullOrEmpty();
           
        }

        [Fact]
        public async Task CreatePost_Test()
        {
            var countBefore = UsingDbContext(a => a.Post.Count());
            CreatePostDto postDto = new CreatePostDto()
            {
                AuthorId = UsingDbContext<User>(a => a.User.FirstOrDefault()).Id,
                Content = "HI",
                Title = "title5"
            };
            var flag = await _postAppService.CreatePost(postDto);
            Assert.True(flag);
            var count = UsingDbContext(a => a.Post.Count());
            Assert.Equal(countBefore+1, count);
        }

        [Fact]
        public async Task GetPostsByAuthorId_Test()
        {
            var authorId = UsingDbContext<Post>(a => a.Post.FirstOrDefault()).AuthorId;
             var postDtos=   await  _postAppService.GetPostsByAuthorId(authorId);

        }
    }
}

