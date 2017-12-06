using Abp.Modules;
using FishFourm.Application;
using FishFourm.EntityFramework;

namespace FishFourm.Test
{
    [DependsOn(
         typeof(FishFourmEntityFrameworkMoulde),
         typeof(FishFourmApplicationMoulde)
     )]
    public  class FishFourmTestModule : AbpModule
    {
    }
}
