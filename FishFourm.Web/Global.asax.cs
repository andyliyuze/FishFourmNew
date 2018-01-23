using Abp.Web;
using FishFourm.Web.App_Start;
using System;

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
