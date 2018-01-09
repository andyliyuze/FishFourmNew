using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;
using FishFourm.WebApi.App_Start;
using Abp.Owin;
using Abp.WebApi.Configuration;
using Abp;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(FishFourm.WebApi.Startup))]

namespace FishFourm.WebApi
{
    public class Startup
    {
        public  void Configuration(IAppBuilder appBuilder)
        {   
            var bootstrapper = AbpBootstrapper.Create<FishFourmWebApiModule>();
            bootstrapper.Initialize();

            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            // Configure Web API for Self-Host
            var httpConfig = bootstrapper.IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;

            ConfigureOAuth(appBuilder);
            appBuilder.UseWebApi(httpConfig);
          
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //显然如果颁发JWT的issuer，这里的issuer不一致的话
            //也是无法通过验证
            var issuer = "http://jwtauthzsrv.azurewebsites.net";

            //显然如果颁发JWT的audience，这里的aud不一致的话
            //也是无法通过验证
            var audience = "Fish";

            //显然如果颁发JWT的加密秘钥，与解密秘钥不一致的话，得到的签名就会
            //不一致，从而资源服务器会认为jwt被篡改
            //从而无法通过验证
            string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["JwtSecret"].ToString();
            var secret = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience, "any" },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                });
        }
    }
}
