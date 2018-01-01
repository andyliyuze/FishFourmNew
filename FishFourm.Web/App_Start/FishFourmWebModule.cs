using Abp.Modules;
using Abp.Web.Mvc;
using FishFourm.EntityFramework;
using FishFourm.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FishFourm.Web.App_Start
{
    [DependsOn(
      typeof(FishFourmWebApiModule),
      typeof(AbpWebMvcModule))]
    public class FishFourmWebModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}