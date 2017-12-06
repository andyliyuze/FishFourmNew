using Abp.Dependency;
using Abp.Web;
using Castle.Facilities.Logging;
using FishFourm.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FishFourm.Web
{
    public class MvcApplication : AbpWebApplication<FishFourmWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);          
        }
    }
}
