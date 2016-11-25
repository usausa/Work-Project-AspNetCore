namespace WebApplication
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    ///
    /// </summary>
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseUrls("http://*:80/")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
