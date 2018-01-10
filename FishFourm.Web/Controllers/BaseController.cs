using Abp.Web.Mvc.Controllers;
using FishFourm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
 

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

        public BaseController()
        {
           
        }
    }
}