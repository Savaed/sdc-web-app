using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using NLog.Web;
using System;
using SDCWebApp.Helpers.Constants;

namespace SDCWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Init Main().");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stoped program because of exception.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(configuration =>
                {
                    configuration.ClearProviders();
                    configuration.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    // Add Azure App Configuration service only if the app is deployed in the cloud i.e. not in the development environment.
                    // Otherwise use locally stored secrets.
                    if (!hostContext.HostingEnvironment.IsDevelopment())
                    {
                        config.AddAzureAppConfiguration(x => x.ConnectWithManagedIdentity(ApiConstants.AzureAppConfigEndpoint)).Build();
                    }
                });
    }
}
