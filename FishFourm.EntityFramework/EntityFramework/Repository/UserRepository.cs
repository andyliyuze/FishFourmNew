using Abp.EntityFramework;
using FishFourm.Core.Entity;
using FishFourm.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.EntityFramework.Repository
{
    public class UserRepository : FishFourmRepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(IDbContextProvider<FishFourmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {


        }
    }
}
