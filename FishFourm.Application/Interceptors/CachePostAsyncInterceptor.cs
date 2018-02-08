using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Abp.Runtime.Caching;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using FishFourm.Application.Posts.Dtos;
using System.Linq;

namespace FishFourm.Application.Interceptors
{
    /// <summary>
    /// 缓存post的拦截器
    /// </summary>
    public class CachePostAsyncInterceptor : IInterceptor
    {

        private readonly ILogger _logger;
        private readonly ICacheManager _cacheManager;

        public CachePostAsyncInterceptor(ILogger logger, ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var cacheAttr = CacheAttribute.GetCacheAttributeOrNull(invocation.MethodInvocationTarget);
            if (cacheAttr == null)
            {
                invocation.Proceed();
                return;
            }

            if (IsAsyncMethod(invocation.Method))
            {
                InterceptAsync(invocation);
            }
            else
            {
                InterceptSync(invocation);
            }
        }

        private async void InterceptAsync(IInvocation invocation)
        {
            var methodName = invocation.MethodInvocationTarget.Name.ToUpper();
            string key = string.Empty;
            string loggerInfo = string.Empty;
            string cacheAction = string.Empty;

            if (methodName == "ReadPost".ToUpper())
            {
                key = string.Format("PostDto:Id:{0}", invocation.GetArgumentValue(0).ToString());
                HandleGetAllOrRead(invocation, key, out cacheAction);
            }

            if (methodName == "GetAllPost".ToUpper())
            {
                key = "AllPosts";

                HandleGetAllOrRead(invocation, key, out cacheAction);
            }

            if (methodName == "UpdatePost".ToUpper() || methodName == "CreatePost".ToUpper())
            {
                cacheAction = "更新集合缓存,";
                Handle(invocation, () => { CreateOrUpdateSucceed(invocation); });
            }

            if (methodName == "DeletePost")
            {
                cacheAction = "更新集合缓存";
                Handle(invocation, () => { DeleteSucceed(invocation); });
            }

            if (methodName == "GetPostsByAuthorId")
            {
                var Icache = _cacheManager.GetCache("post");
               
                //获取所有的帖子集合缓存
                var allpostsTask =  Icache.GetOrDefault("AllPosts");

                if (allpostsTask != null)
                {
                    cacheAction = "从缓存集合获取数据";
                    var authorId = (Guid)invocation.Arguments[0];
                    var allposts = ((Task<IList<PostOutput>>)allpostsTask).Result;
                    invocation.ReturnValue = Task.FromResult<IEnumerable<PostOutput>>(allposts.Where(a => a.AuthorId == authorId));
                    return;
                }
                cacheAction = "从数据库获取数据";
                invocation.Proceed();
                ((Task)invocation.ReturnValue).Wait();
            }
            //After method execution   
            loggerInfo = string.Format("执行了{0}方法, 缓存行为：{1}", methodName, cacheAction);
            await Task.Run(() =>
            {
                _logger.InfoFormat(loggerInfo);
            });

            return;
        }

        private void InterceptSync(IInvocation invocation)
        {
            //Before method execution
            //Executing the actual method
            invocation.Proceed();
            //After method execution      
        }

        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }

        private void DeleteSucceed(IInvocation invocation)
        {
            var postId = invocation.GetArgumentValue(0).ToString();

            var key = string.Format("PostDto:Id:{0}", postId);

            var Icache = _cacheManager.GetCache("post");

            //从实体缓存移除
            Icache.Remove(key);

            //从集合缓存移除
            var allposts = ((Task<IList<PostOutput>>)Icache.GetOrDefault("AllPosts")).Result;

            if (allposts != null)
            {
                var postDTO = allposts.FirstOrDefault(a => a.Id.ToString() == postId);
                if (postDTO != null)
                {
                    allposts.Remove(postDTO);
                }
                Icache.Set("AllPosts", Task.FromResult(allposts));
            }
        }

        private void CreateOrUpdateSucceed(IInvocation invocation)
        {
            var postDto = ((Task<PostOutput>)invocation.ReturnValue).Result;

            var Icache = _cacheManager.GetCache("post");
            //添加到集合缓存
            var allpostsTask = ((Task<IList<PostOutput>>)Icache.GetOrDefault("AllPosts"));

            if (allpostsTask != null)
            {
                var allposts = allpostsTask.Result;
                var oldDTO = allposts.Where(a => a.Id == postDto.Id).FirstOrDefault();
                allposts.Remove(oldDTO);
                allposts.Add(postDto);
                Icache.Set("AllPosts", Task.FromResult(allposts));
            }

        }

        private void HandleGetAllOrRead(IInvocation invocation, string key, out string cacheAction)
        {
            var Icache = _cacheManager.GetCache("post");

            var cache = Icache.GetOrDefault(key);

            if (cache == null)
            {
                Handle(invocation,()=>
                {
                    var result = invocation.ReturnValue;
                    Icache.Set(key, result);
                });               
                cacheAction = "从数据库获取数据并添加到缓存中";
            }
            else
            {
                cacheAction = "从缓存获取了数据";
                invocation.ReturnValue = cache;
            }
        }

        /// <summary>
        /// 公共处理方法
        /// </summary>
        /// <param name="invocation">业务方法上下文</param>
        /// <param name="action">异步业务方法成功后的回调函数</param>
        private void Handle(IInvocation invocation, Action action)
        {
            invocation.Proceed();

            ((Task)invocation.ReturnValue).ContinueWith(task =>
            {
                if (((Task)invocation.ReturnValue).IsFaulted) { return; }
                action();
            });

        }
    }
}