using ExpenseTracker.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace ExpenseTracker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var config = Config.Get;
                CreateHostBuilder(args, config).Build().Run();
            }
            catch (Exception ex)
            {
                Common.Logger.Log($"An exception occurred while starting web service: {ex.Message}", LogLevel.ERROR);
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args, Config config) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls($"http://{config.ServerAddress}:{config.ServerPort}/");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
