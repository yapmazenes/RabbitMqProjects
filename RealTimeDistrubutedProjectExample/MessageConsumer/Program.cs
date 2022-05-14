using MessageConsumer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace MessageConsumer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration((config) => { config.AddEnvironmentVariables(prefix: "ASPNETCORE_"); })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", false)
                    .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

                    if (args != null)
                        config.AddCommandLine(args);

                    config.Build();

                })
                .ConfigureServices((hostContext, services) => services.AddServices(hostContext.Configuration))
                .Build().RunAsync();

            Console.WriteLine("Message Consumer Started...");
        }
    }
}
