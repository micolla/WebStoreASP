using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace WebStore.ServiceHosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .UseSerilog((host, log) =>
            {
                log.ReadFrom.Configuration(host.Configuration)
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .WriteTo.Console();
            });
    }
}
