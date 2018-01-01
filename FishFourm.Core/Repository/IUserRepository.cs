using Abp.Domain.Repositories;
using FishFourm.Core.Entity;
using System;
using System.Threading.Tasks;

namespace FishFourm.Core.Repository
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<Guid> CreateUser(User user);
    }
}
