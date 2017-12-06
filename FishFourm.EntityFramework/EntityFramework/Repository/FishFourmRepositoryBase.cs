using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.EntityFramework.Repository
{


    /// <summary>
    /// 定义了泛型基类
    /// </summary>
    /// <typeparam name="TEntity">实体的类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    public class FishFourmRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<FishFourmDbContext, TEntity, TPrimaryKey>
               where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FishFourmRepositoryBase(IDbContextProvider<FishFourmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
