using Abp.Domain.Repositories;
using FishFourm.Core.Entity;
using System;

namespace FishFourm.Core.Repository
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
