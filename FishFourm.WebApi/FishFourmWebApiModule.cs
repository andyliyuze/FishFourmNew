using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.WebApi;
using FishFourm.Application;
using FishFourm.EntityFramework;
using FishFourm.WebApi.App_Start;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FishFourm.WebApi
{
    [DependsOn(
        typeof(AbpWebApiModule),
       typeof(FishFourmApplicationMoulde)
      , typeof(FishFourmEntityFrameworkMoulde)
     
       )]
    public class FishFourmWebApiModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<FishFourmDbContext, FishFourmDbContext>(DependencyLifeStyle.Transient);
            Configuration.Modules.AbpWebApi().HttpConfiguration = new HttpConfiguration();
        }

        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
             IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            var config = Configuration.Modules.AbpWebApi().HttpConfiguration;
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.MapHttpAttributeRoutes();
            SwaggerConfig.Register(config);
        }
    }
}
