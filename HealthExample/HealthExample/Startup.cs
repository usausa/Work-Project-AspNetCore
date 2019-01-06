using System.Threading.Tasks;
using HealthExample.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace HealthExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // https://github.com/xabaril/AspNetCore.Diagnostics.HealthChecks
            services.AddHealthChecks()
                //.AddSqlServer(Configuration["ConnectionStrings:DefaultConnection"])
                //.AddRedis(Configuration["Data:ConnectionStrings:Redis"])
                .AddCheck<ExampleHealthCheck>("example")
                .AddCheck<MemoryHealthCheck>("memory")
                .AddCheck("test", () => HealthCheckResult.Healthy("test is OK!"), tags: new[] { "test" });

            // Default singleton
            services.AddSingleton<ExampleHealthCheck>();

            services.AddSingleton<IHealthCheckPublisher, DebugPublisher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                AllowCachingResponses = false,
                ResponseWriter = WriteResponse,
                //Predicate = check => check.Tags.Contains("test"),
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
            app.UseHealthChecks("/health/test", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("test")
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static async Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            var json = JsonConvert.SerializeObject(result);

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(json);

            //httpContext.Response.ContentType = "application/json";
            //var json = new JObject(
            //    new JProperty("status", result.Status.ToString()),
            //    new JProperty("results", new JObject(result.Entries.Select(pair =>
            //        new JProperty(pair.Key, new JObject(
            //            new JProperty("status", pair.Value.Status.ToString()),
            //            new JProperty("description", pair.Value.Description),
            //            new JProperty("data", new JObject(pair.Value.Data.Select(
            //                p => new JProperty(p.Key, p.Value))))))))));
            //return httpContext.Response.WriteAsync(
            //    json.ToString(Formatting.Indented));
        }
    }
}
