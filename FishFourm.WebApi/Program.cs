using Abp;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            const string uri = "http://localhost:5256/";
            using (WebApp.Start(uri, Startup.Configuration))
            {
              
                Console.WriteLine("Started listening on " + uri);
                Console.ReadLine();
                Console.WriteLine("Shutting down...");
            }
            Console.ReadLine();
        }
    }
}
