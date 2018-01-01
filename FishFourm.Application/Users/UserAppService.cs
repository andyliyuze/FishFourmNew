using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFourm.Application.Users.DTO;
using Abp.Application.Services;
using FishFourm.Core.Repository;
using AutoMapper;
using FishFourm.Core.Entity;

namespace FishFourm.Application.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {

        private readonly IUserRepository _userRepository;
        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Guid> CreateUser(UserDTO userDTO)
        {
            var user = Mapper.Map<User>(userDTO);
            await _userRepository.InsertAsync(user);
            return user.Id;
        }

        public async Task<Guid> UpdateUser(UserDTO userDTO)
        {
            var user = await _userRepository.FirstOrDefaultAsync(userDTO.Id);
            if (user == null)
            {
                throw new Exception("不存在该用户");
            }

            if (string.IsNullOrWhiteSpace(userDTO.UserName))
            {
                throw new Exception("用户名不能为空");
            }

            user.Update(userDTO.UserName);
          
            return user.Id;
        }
    }
}
