using Abp.Modules;
using FishFourm.Application;
using FishFourm.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.WindowsService
{
    [DependsOn(typeof(FishFourmApplicationMoulde), typeof(FishFourmEntityFrameworkMoulde))]
    public class FishFourmWinServiceModule : AbpModule
    {
        public override void PreInitialize()
        {
           
        }
        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
