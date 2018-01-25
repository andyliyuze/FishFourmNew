using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Common
{
   public interface IWebApiResponseHandle
    {
        /// <summary>
        /// api接口响应的统一接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> CreateHttpResponse<T>(HttpRequestMessage request, Func<Task<T>> function);
    }
}
