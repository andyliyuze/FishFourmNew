using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;
using FishFourm.WebApi.App_Start;
using Abp.Owin;
using Abp.WebApi.Configuration;
using Abp;

[assembly: OwinStartup(typeof(FishFourm.WebApi.Startup))]

namespace FishFourm.WebApi
{
    public class Startup
    {
        public static void Configuration(IAppBuilder appBuilder)
        {   
            var bootstrapper = AbpBootstrapper.Create<FishFourmWebApiModule>();
            bootstrapper.Initialize();

            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            // Configure Web API for Self-Host
            var httpConfig = bootstrapper.IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;          
            appBuilder.UseWebApi(httpConfig);
          
        }
    }
}
