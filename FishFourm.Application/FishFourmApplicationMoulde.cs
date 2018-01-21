using Abp.Dependency;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using FishFourm.Application.Interceptors;
using FishFourm.Application.Posts;
using FishFourm.Application.Posts.Dtos;
using FishFourm.Core;
 
using System.Reflection;

namespace FishFourm.Application
{
    [DependsOn(typeof(FishFourmCoreMoulde))]
    public  class FishFourmApplicationMoulde:AbpModule
    {
        public override void PreInitialize()
        {
            MapConfig.Config();
            //IocManager.IocContainer.Register(
            //    Component.For<MeasureDurationInterceptor>().LifestyleTransient(),
            //    Component.For<IPostAppService>().ImplementedBy<PostAppService>()
              
            //                    .Interceptors<MeasureDurationInterceptor>()
            //    );
          
           MeasureDurationInterceptorRegistrar.Initialize(IocManager.IocContainer.Kernel);
        }
        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
 
        }
    }
}
