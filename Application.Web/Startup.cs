namespace Application.Web
{
    using System.Text;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using NLog.Extensions.Logging;
    using NLog.Web;

    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        private IServiceCollection serviceCollection;

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddOptions();

            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                });

            // ELM
            services.AddElm(options =>
            {
                options.Filter = (name, lelev) => lelev >= LogLevel.Debug;
            });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("application", new Info { Title = "Application API", Version = "v1" });
                options.DescribeAllEnumsAsStrings();    // Enum
            });

            serviceCollection = services;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                // for Debug
                app.Map("/services", builder => builder.Run(async context =>
                {
                    var sb = new StringBuilder();
                    sb.Append("<h1>All Services</h1>");
                    sb.Append("<table><thead>");
                    sb.Append("<tr><th>Lifetime</th><th>Create by</th><th>Service Type</th><th>Implement Type</th></tr>");
                    sb.Append("</thead><tbody>");
                    foreach (var service in serviceCollection)
                    {
                        string createBy;
                        string implementType;
                        if (service.ImplementationType != null)
                        {
                            createBy = "Type";
                            implementType = service.ServiceType == service.ImplementationType
                                ? string.Empty
                                : service.ImplementationType.FullName;
                        }
                        else if (service.ImplementationFactory != null)
                        {
                            createBy = "Factory";
                            implementType = string.Empty;
                        }
                        else
                        {
                            createBy = "Constant";
                            implementType = service.ImplementationInstance.GetType().FullName;
                        }

                        sb.Append("<tr>");
                        sb.Append($"<td>{service.Lifetime}</td>");
                        sb.Append($"<td>{createBy}</td>");
                        sb.Append($"<td>{service.ServiceType.FullName}</td>");
                        sb.Append($"<td>{implementType}</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody></table>");
                    await context.Response.WriteAsync(sb.ToString());
                }));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/application/swagger.json", "Application API");
                });
            }
        }
    }
}
