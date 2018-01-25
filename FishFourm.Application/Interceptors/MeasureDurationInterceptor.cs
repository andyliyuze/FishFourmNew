using Abp.Application.Services;
using Abp.Runtime.Caching;
using Castle.Core;
using Castle.Core.Interceptor;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using FishFourm.Application.Posts;
using FishFourm.Application.Posts.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Application.Interceptors
{
    public class MeasureDurationInterceptor : IInterceptor
    {
        private readonly ILogger _logger;
        private readonly ICacheManager _cacheManager;

        public MeasureDurationInterceptor(ILogger logger, ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            //Before method execution
            string key="";
            if (invocation.MethodInvocationTarget.Name == "ReadPost")
            {
                key = string.Format("PostDto:Id:{0}", invocation.GetArgumentValue(0).ToString());
            }
            if (invocation.MethodInvocationTarget.Name == "GetAllPost")
            {
                key = "AllPosts";
            }
            var Icache = _cacheManager.GetCache("post");

            var cache = Icache.GetOrDefault(key);

            var getResultWay = "Cache";

            if (cache == null)
            {
                invocation.Proceed();

                var type = invocation.Method.ReturnType;
                var a = typeof(string);
                //var resultw =( (Task<object>)invocation.ReturnValue ).Result  ;

                var result = invocation.ReturnValue ;
                Icache.Set(key, result);
                getResultWay = "Database";
            }
            else 
            {
                invocation.ReturnValue = cache;
            }
            //Executing the actual method          
           
           
            // invocation.Proceed();
            //After method execution         
            _logger.InfoFormat(
                "{0} executed and return with {1}.",
                invocation.MethodInvocationTarget.Name, getResultWay);
            return;
        }
    }

    public static class MeasureDurationInterceptorRegistrar
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(IPostAppService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
               // handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(MeasureDurationInterceptor)));
                  handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(CachePostAsyncInterceptor)));
            }
        }
    }
}
