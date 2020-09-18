using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Winton.Extensions.Configuration.Consul;
using Serilog;

namespace AspNetCore.Consul.Config
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddConsul(
                "WebApi/appsettings.json",
                options =>
                {
                    options.ConsulConfigurationOptions =
                        cco => { cco.Address = new Uri("http://consul:8500"); };
                    options.Optional       = true;
                    options.PollWaitTime   = TimeSpan.FromSeconds(5);
                    options.ReloadOnChange = true;
                })
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            // TODO: Start Consul with an inicial appsettings.json
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            try
            {
                Log.Information($"Starting {Configuration["Serilog:Properties:ApplicationName"]}");
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Error al configurar");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .AddConsul(
                            "WebApi/appsettings.json",
                            options =>
                            {
                                options.ConsulConfigurationOptions =
                                    cco => { cco.Address = new Uri("http://consul:8500"); };
                                options.Optional = true;
                                options.PollWaitTime = TimeSpan.FromSeconds(5);
                                options.ReloadOnChange = true;
                            })
                        .AddEnvironmentVariables();
                });
    }
}
