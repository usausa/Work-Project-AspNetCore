namespace Application.Web
{
    using System.Text;

    using Application.Components.Serializer;
    using Application.Web.Api;
    using Application.Web.Mvc;

    using AutoMapper;

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
        public IConfiguration Configuration { get; }

        private IServiceCollection serviceCollection;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            env.ConfigureNLog("nlog.config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            serviceCollection = services;

            // Add framework services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
                options.Path = new PathString("/elm");
                options.Filter = (name, lelev) => lelev >= LogLevel.Trace;
            });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("application", new Info { Title = "Application API", Version = "v1" });
                options.DescribeAllEnumsAsStrings();    // Enum
                // TODO additional?
            });

            // Settings
            // TODO

            // Components

            // Data
            // TODO

            // Security
            // TODO

            // Serializer
            services.AddSingleton<IStringSerializer, JsonStringSerializer>();

            // Mail
            // TODO

            // Report
            // TODO

            // Image
            // TODO

            // View
            // TODO(SelectListBuilder, API Token?)

            // Mapper
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(c =>
            {
                c.AddProfile<MvcMappingProfile>();
                c.AddProfile<ApiMappingProfile>();
            })));

            // Services
            // TODO
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, ILoggerFactory loggerFactory)
        {
            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopping.Register(OnStopping);
            lifetime.ApplicationStopped.Register(OnStopped);

            loggerFactory.AddNLog();

            app.AddNLogWeb();

            // ELM
            if (env.IsDevelopment())
            {
                app.UseElmPage();
                app.UseElmCapture();
            }

            // Custom
            if (env.IsDevelopment())
            {
                UseServices(app);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Swagger
            //if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/application/swagger.json", "Application API");
                });
            }
        }

        private void UseServices(IApplicationBuilder app)
        {
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

        private void OnStarted()
        {
            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            // Perform post-stopped activities here
        }
    }
}
