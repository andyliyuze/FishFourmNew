using Abp;
using MassTransit.Log4NetIntegration.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FishFourm.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
                // MassTransit to use Log4Net
                Log4NetLogger.Use();
                HostFactory.Run(cfg => cfg.Service(x => new UserService()));    
        }
    }
}
