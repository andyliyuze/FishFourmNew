using Abp.WebApi.Controllers;
using FishFourm.Application.Users.DTO;
using FishFourm.Common;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace FishFourm.WebApi.Controllers
{
    public class BaseApiController : AbpApiController
    {
        protected readonly IWebApiResponseHandle _webApiResponseHandle;
        public BaseApiController(IWebApiResponseHandle webApiResponseHandle)
        {
            _webApiResponseHandle = webApiResponseHandle;
        }

        /// <summary>
        /// 已验证的用户信息
        /// </summary>
        protected virtual UserDTO UserInfo
        {
            get
            {
                var principal = RequestContext.Principal as ClaimsPrincipal;
                var userData = principal.Claims.Where(a => a.Type == ClaimTypes.UserData).FirstOrDefault();
                if (userData != null)
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<UserDTO>(userData.Value);
                    }
                    catch { return null; }
                }
                return null;
            }
        }

    }
}
