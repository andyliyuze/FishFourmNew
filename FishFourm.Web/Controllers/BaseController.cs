using Abp.Web.Mvc.Controllers;
using FishFourm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace FishFourm.Web.Controllers
{
    public class BaseController : AbpController
    {
        protected virtual HttpClientHepler HttpClientHepler
        {
            get
            {
                var token = Request.Cookies.Get("token");
                if (token != null)
                {
                    return new HttpClientHepler("Bearer", token.Value);
                }
                else
                {
                    return new HttpClientHepler();
                }
            }
        }

        protected virtual ActionResult CreateRequest(Func<HttpResponseMessage> func)
        {
            var response = func.Invoke();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("/users/login");
            }
            return null;
        }
    }
}