namespace TcpServerExample
{
    using System;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Connections;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;

    using NLog.Web;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            Console.ReadLine();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);

                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                    }
                })
                .UseNLog()
                .UseKestrel(options =>
                {
                    options.ListenAnyIP(10000, builder =>
                    {
                        builder.UseConnectionHandler<ServiceConnectionHandler>();
                    });
                })
                .UseStartup<Startup>();
    }
}
