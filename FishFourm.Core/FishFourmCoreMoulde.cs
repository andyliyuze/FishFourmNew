using Abp.Modules;
using System.Reflection;

namespace FishFourm.Core
{
    public class FishFourmCoreMoulde : AbpModule
    {


        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
