using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.RouteAnalyzer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AreaExample
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddRouteAnalyzer();
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

            app.UseMvc(routes =>
            {
                routes.MapRouteAnalyzer("/routes");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}",
                    defaults: new { area = "Default" });

                routes.MapAreaRoute(
                    name: "Api",
                    areaName: "Api",
                    template: "Api/{controller=Home}/{action=Index}/{id?}");
                routes.MapAreaRoute(
                    name: "SubSystem1",
                    areaName: "SubSystem1",
                    template: "SubSystem1/{controller=Home}/{action=Index}/{id?}");
                routes.MapAreaRoute(
                    name: "SubSystem2",
                    areaName: "SubSystem2",
                    template: "SubSystem2/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
