using Abp.Modules;
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
            MeasureDurationInterceptorRegistrar.Initialize(IocManager.IocContainer.Kernel);

        }
        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
