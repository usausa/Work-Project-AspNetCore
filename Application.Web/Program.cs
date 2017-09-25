namespace Application.Web
{
    using System.IO;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true) // Capture Startup Errors
                //.UseSetting(WebHostDefaults.DetailedErrorsKey, "true")  // Detailed Errors
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
