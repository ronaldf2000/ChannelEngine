using ChannelEngine.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace ChannelEngine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(cb =>
                    cb.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args))
                .ConfigureServices((hostContext, services) =>
                {
                    string baseUrl = hostContext.Configuration["ApiBaseUrl"];
                    if(string.IsNullOrEmpty(baseUrl))
                    {
                        throw new Exception("Missing 'ApiBaseUrl' in configuration");
                    }
                    services.AddServices(baseUrl);
                    services.AddHostedService<RetreiveOrders>();
                });
        }
    }
}
