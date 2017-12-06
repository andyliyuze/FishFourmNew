using Abp.Modules;
using System;
using FishFourm.Core;
using Abp.EntityFramework;
using System.Reflection;
using System.Data.Entity;

namespace FishFourm.EntityFramework
{
    [DependsOn(typeof(FishFourmCoreMoulde), typeof(AbpEntityFrameworkModule))]
    public class FishFourmEntityFrameworkMoulde : AbpModule
    {
        public override void PreInitialize()
        {
            //  Database.SetInitializer(new CreateDatabaseIfNotExists<FishFourmDbContext>());
              //不能要
            //Configuration.DefaultNameOrConnectionString = "FishFourm";
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
