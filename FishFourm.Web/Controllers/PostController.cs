using Abp.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Json;
using FishFourm.Common;
using Newtonsoft.Json;
using FishFourm.Application.Posts.Dtos;
using FishFourm.Web.Attribute;

namespace FishFourm.Web.Controllers
{


    public class PostController : BaseController
    {
        private const string URL = "http://localhost:5256/api/post/";

       [CustomAuthorizeAttribute]
        // GET: Post
        public async Task<ActionResult> Index()
        {
            var posts = await PostsList();
           
            ViewBag.posts = posts;
            return View();
        }

        /// <summary>
        ///  调用webapi
        /// </summary>
        /// <returns></returns>

        private async Task<object> PostsList()
        {
            var result = await HttpClientHepler.GetAsync(URL + "postList");
            if (result.StatusCode == WebApiStatusCode.Unauthorized)
            {
                return "Unauthorized";
            }
            return result;
        }

        public async Task<ActionResult> Detail(Guid id)
        {
            ViewBag.Detail = await GetDetailApi(id);
            return View();
        }

        /// <summary>
        /// 调用webapi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   
        private async Task<string> GetDetailApi(Guid id)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URL + string.Format("readPost?id={0}", id)))
                {
                    // client_id and client_secret: http://tools.ietf.org/html/rfc6749#section-2.3.1
                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
        }

        [HttpGet]
        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Create(CreatePostDto postInput)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URL + string.Format("createPost")))
                {
                    var values = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("AuthorId", postInput.AuthorId.ToString()),
                    new KeyValuePair<string, string>("Title", postInput.Title),
                    new KeyValuePair<string, string>("Content", postInput.Content),
                };
                    request.Content = new FormUrlEncodedContent(values);
                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
        }
    }
}