namespace TcpServerExample
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Connections;
    using Microsoft.AspNetCore.Hosting;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            Console.ReadLine();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
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
