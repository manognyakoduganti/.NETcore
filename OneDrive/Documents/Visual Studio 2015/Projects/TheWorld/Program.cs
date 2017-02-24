using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace TheWorld
{
    public class Program
    {
        // run starts here
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                // webserver
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                //for headers
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
