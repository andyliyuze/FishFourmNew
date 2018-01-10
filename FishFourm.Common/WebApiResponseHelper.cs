
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FishFourm.Common
{
    public class WebApiResponseHelper
    {
        public async Task<HttpResponseMessage> CreateHttpResponse<T>(HttpRequestMessage request, Func<Task<T>> function)
        {
            HttpResponseMessage response = null;

            try
            {
                var result = await function.Invoke();
                var resp = new WebApiResponse<T>() { Result = result, StatusCode = WebApiStatusCode.Success, Msg = "OK" };
                response = request.CreateResponse<WebApiResponse<T>>(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                var resp = new WebApiResponse<string>() { Result = "", StatusCode = WebApiStatusCode.Failed, Msg = JsonConvert.SerializeObject(ex) };
                response = request.CreateResponse<WebApiResponse<string>>(HttpStatusCode.OK, resp);
            }

            return response;
        }
    }
}
