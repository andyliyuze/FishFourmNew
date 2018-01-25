namespace FishFourm.Common
{

    /// <summary>
    /// 返回统一结果
    /// </summary>
    /// <typeparam name="T">T是返回结果的类型</typeparam>
    public class WebApiResponse<T>
    {
        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public WebApiStatusCode StatusCode { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }
    }

    public enum WebApiStatusCode
    {
        Success = 200,
        Failed = 500,
        Unauthorized = 404
    }
}
