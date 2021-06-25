using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace MongoDB
{
    public class Program
    {                    
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var startup = scope.ServiceProvider.GetRequiredService<Startup>();
            startup.Init();

            ///host.RunAsync();
        }
    
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, appConfig) => {

                var env = hostContext.HostingEnvironment;
                appConfig.SetBasePath(Directory.GetCurrentDirectory());
                appConfig
                    .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true,
                        reloadOnChange: true);

            })
            .ConfigureServices((hostContext, servicesCollection) =>
            {
                servicesCollection.Configure<IConfiguration>(hostContext.Configuration);           
                servicesCollection.AddScoped<Startup>();
            });
    }
}
