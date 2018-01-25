using AutoMapper;
using FishFourm.Application.Coments.DTO;
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
                cfg.CreateMap<Post, PostOutput>();
                cfg.CreateMap<User, PostOutput>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d=>d.AuthorId ,opt=>opt.MapFrom(s=>s.Id));

                cfg.CreateMap<Comment, CommentOutput>();

            });
        }
    }
}
