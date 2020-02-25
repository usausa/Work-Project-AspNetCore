namespace Application.Web
{
    using System;

    using Application.Web.Api;
    using Application.Web.Mvc;

    using AutoMapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using NLog.Extensions.Logging;
    using NLog.Web;

    using Smart.AspNetCore.Filters;
    using Smart.Resolver;

    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            env.ConfigureNLog("nlog.config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            services.AddExceptionLogging();
            services.AddTimeLogging(options =>
            {
                options.Thresold = 5000;
            });

            services
                .AddMvc(options =>
                {
                    options.Filters.AddExceptionLogging();
                    options.Filters.AddTimeLogging();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("application", new Info { Title = "Application API", Version = "v1" });
                options.DescribeAllEnumsAsStrings();    // Enum
            });

            // Mapper
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(c =>
            {
                c.AddProfile<MvcMappingProfile>();
                c.AddProfile<ApiMappingProfile>();
            })));

            // Replace activator
            services.AddSingleton<IControllerActivator, SmartResolverControllerActivator>();
            services.AddSingleton<IViewComponentActivator, SmartResolverViewComponentActivator>();

            // Add application services
            var config = new ResolverConfig();
            SetupComponents(config);

            // Use custom service provider
            return SmartResolverHelper.BuildServiceProvider(config, services);
        }

        private void SetupComponents(ResolverConfig config)
        {
            // TODO
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            app.AddNLogWeb();

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
                    name: "area",
                    template: "{area:exists}/{controller}/{action=Index}/{id?}");
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
    }
}
