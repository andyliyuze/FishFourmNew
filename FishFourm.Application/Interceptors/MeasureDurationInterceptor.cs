using Abp.Application.Services;
using Castle.Core;
using Castle.Core.Interceptor;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Castle.MicroKernel;
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

        public MeasureDurationInterceptor(ILogger logger)
        {
         //   Logger = NullLogger.Instance;
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            //Before method execution
            var stopwatch = Stopwatch.StartNew();

            //Executing the actual method
            invocation.ReturnValue = null;
            
            invocation.Proceed();
            //After method execution
            stopwatch.Stop();
            _logger.InfoFormat(
                "{0} executed in {1} milliseconds.",
                invocation.MethodInvocationTarget.Name,
                stopwatch.Elapsed.TotalMilliseconds.ToString("0.000")
                );
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
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(MeasureDurationInterceptor)));
            //    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(MeasureDurationAsyncInterceptor)));
            }
        }
    }
}
