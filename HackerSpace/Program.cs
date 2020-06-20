using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HackerSpace.Utilities;

namespace HackerSpace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //See https://stackoverflow.com/questions/43577178/how-to-access-sqlite-db-from-iis-hosted-wcf-service/43598678
            //CurrentDirectoryHelpers.SetCurrentDirectory();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
