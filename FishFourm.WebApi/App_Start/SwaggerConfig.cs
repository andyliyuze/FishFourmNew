using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using FishFourm.WebApi.App_Start;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace FishFourm.WebApi.App_Start
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "webApiToken");
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                //   c.IncludeXmlComments(SwaggerConfig.GetXmlCommentsPath());
            })
            .EnableSwaggerUi();
        }
        
        protected static string GetXmlCommentsPath()
        {
           
            return string.Format(@"{0}\webApiToken.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }

    }
}
