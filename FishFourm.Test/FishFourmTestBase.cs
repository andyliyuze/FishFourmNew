using Abp.Domain.Uow;
using Abp.TestBase;
using AutoMapper;
using Castle.MicroKernel.Registration;
using EntityFramework.DynamicFilters;
using FishFourm.EntityFramework;
using FishFourm.Test.InitialData;
using System;
using System.Collections.Generic;
 
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace FishFourm.Test
{
   public abstract class FishFourmTestBase: AbpIntegratedTestBase<FishFourmTestModule>
    {
        protected FishFourmTestBase()
        {
            //Seed initial data
            UsingDbContext(context => new FishFourmInitialDataBuilder().Build(context));
        }
        protected override void PreInitialize()
        {
            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                    .UsingFactoryMethod(Effort.DbConnectionFactory.CreateTransient)
                    .LifestyleSingleton()
                );
            base.PreInitialize();
        }
        public void UsingDbContext(Action<FishFourmDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<FishFourmDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                context.SaveChanges();
            }
        }
        public T UsingDbContext<T>(Func<FishFourmDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<FishFourmDbContext>())
            {
                context.DisableAllFilters();
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        public override void Dispose()
        {
            Mapper.Reset();
            base.Dispose();
        }
    }
}
