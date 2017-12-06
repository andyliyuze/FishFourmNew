using AutoMapper;
using FishFourm.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishFourm.Application.Posts.Dtos
{
   public class MapConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostDto>();
                cfg.CreateMap<User, PostDto>().ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.Name));
            });
        }
    }
}
