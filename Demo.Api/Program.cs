using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {                   
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration(config=> {

                    Dictionary<String, String> maps = new Dictionary<string, string>();
                    //maps.Add("help","h");
                    config.AddJsonFile("secondSetting.json");
                    config.AddCommandLine(args);
                 }).ConfigureLogging(config=>
                 {
                     config.ClearProviders();
                     config.AddConsole();
                     config.AddSeq();
                 });

    }
}


// Action<Object>
// Func
// Predicate