using AutoMapper;
using FishFourm.Application.Posts.Dtos;
using FishFourm.Application.Users.DTO;
using FishFourm.Core.Entity;
using UserSystem.MessageContract;

namespace FishFourm.Application
{
    public class MapConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserRegisted, UserDTO>();
                cfg.CreateMap<UserInfoUpdated, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Post, PostDto>();
                cfg.CreateMap<User, PostDto>().ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.UserName));
            });
        }
    }
}
