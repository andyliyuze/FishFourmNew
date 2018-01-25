using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Modules;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using FishFourm.Application;
using FishFourm.EntityFramework;
using System.Reflection;

namespace FishFourm.Test
{
    [DependsOn(
         typeof(FishFourmEntityFrameworkMoulde),
         typeof(FishFourmApplicationMoulde)
     )]
    public  class FishFourmTestModule : AbpModule
    {
        public override void Initialize()
        {
            //MapConfig.Config();
            IocManager.Register<ILogger, NullLogger>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
