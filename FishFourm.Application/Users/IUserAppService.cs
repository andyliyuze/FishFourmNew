using FishFourm.Application.Users.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Application.Users
{
   public interface IUserAppService
    {
        Task<Guid> CreateUser(UserDTO userDTO);

        Task<Guid> UpdateUser(UserDTO userDTO);
    }
}
