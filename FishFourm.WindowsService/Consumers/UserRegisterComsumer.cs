using AutoMapper;
using FishFourm.Application.Users;
using FishFourm.Application.Users.DTO;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.MessageContract;

namespace FishFourm.WindowsService.Consumers
{
    public class UserRegistedComsumer : IConsumer<UserRegisted>
    {
        private readonly IUserAppService _userAppService;

        public UserRegistedComsumer(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task Consume(ConsumeContext<UserRegisted> context)
        {
            try
            {
                var DTO = Mapper.Map<UserDTO>(context.Message);
                
                await _userAppService.CreateUser(DTO);

                await Console.Out.WriteLineAsync(string.Format("你有新的用户：Id:{0},Name:{1}",
                    context.Message.Id, context.Message.UserName));
            }
            catch (Exception e){ throw e; }
        }
    }

   
}
