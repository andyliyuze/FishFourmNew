using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FishFourm.Web.Attribute
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {



            if (httpContext.Request.Cookies["token"] == null)
            {
                return false;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var clientId = ConfigurationManager.AppSettings["clientId"].ToString();
            var loginUrl = ConfigurationManager.AppSettings["authorizeEndpoint"].ToString();

            string uri = string.Format("{0}?redirect_uri={1}&client_Id={2}&grant_type={3}", 
                loginUrl,filterContext.HttpContext.Request.Url, clientId, "password");
            filterContext.Result = new RedirectResult(uri);
        }
    }
}