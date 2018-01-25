using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FishFourm.Common
{
    public class HttpClientHepler
    {
        private HttpClient _httpClient;

        public HttpClientHepler()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// 设置请求头部
        /// </summary>
        /// <param name="authorizationType">令牌类型</param>
        /// <param name="value">访问令牌</param>
        public HttpClientHepler(string authorizationType, string value)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _httpClient.DefaultRequestHeaders.Add("Authorization",
                string.Format("{0} {1}", authorizationType, value));
        }

        public async Task<WebApiResponse<string>> GetAsync(string url , params object[] args)
        {
            string internalUrl = string.Format(url, args);
            var result = await _httpClient.GetAsync(internalUrl);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new WebApiResponse<string>() { StatusCode = WebApiStatusCode.Unauthorized };
            }
            return await result.Content.ReadAsAsync<WebApiResponse<string>>();
        }

        public async Task<T> PostAysnc<T>(string url, object parm)
        {
            var dic = DictionaryFormatter.MapToDictionary(parm);
            var result = await _httpClient.PostAsync(url,new FormUrlEncodedContent(dic));        
            return await result.Content.ReadAsAsync<T>();        
        }

        public async Task<HttpResponseMessage> PostAysnc<T>(string url, T parm)
        {
            var dic = DictionaryFormatter.MapToDictionary(parm);
            var result = await _httpClient.PostAsync(url, new FormUrlEncodedContent(dic));
            return result;
        }
    
        public void SetTimeout(double seconds)
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(seconds);
        }
    }
}
