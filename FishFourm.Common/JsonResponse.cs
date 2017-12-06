using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishFourm.Common
{
    public class JsonResponse
    {
        /// <summary>
        /// 返回json对象
        /// </summary>
        /// <param name="data">请求数据</param>
        /// <param name="statusCode">请求状态码</param>
        /// <param name="error">错误消息，如果有</param>
        public JsonResponse(object data, int statusCode, string error = null)
        {
            Data = data;
            StatusCode = statusCode;
            Error = error;
        }

        public string Error { get; set; }

        public object Data { get; set; }

        //200,400,500
        public int StatusCode { get; set; }
    }
}