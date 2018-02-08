using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Application.Interceptors
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        internal static CacheAttribute GetCacheAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(CacheAttribute), false);
            if (attrs.Length > 0 &&(typeof(IRepository).IsAssignableFrom(methodInfo.DeclaringType) || typeof(IApplicationService).IsAssignableFrom(methodInfo.DeclaringType)))
            {
                return (CacheAttribute)attrs[0];
            }
   
            return null;
        }
    }
}
