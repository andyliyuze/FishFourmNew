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
    public class UserUpdatedConsumer : IConsumer<UserInfoUpdated>
    {
        private readonly IUserAppService _userAppService;

        public UserUpdatedConsumer(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task Consume(ConsumeContext<UserInfoUpdated> context)
        {
            try
            {
                var DTO = Mapper.Map<UserDTO>(context.Message);

                await _userAppService.UpdateUser(DTO);

                await Console.Out.WriteLineAsync(string.Format("用户更新：Id:{0},Name:{1}",
                    context.Message.Id, context.Message.UserName));
            }
            catch (Exception e) { throw e; }
        }
    }
}
