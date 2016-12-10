namespace WebApplication
{
    using System;
    using System.Buffers;
    using System.IO;

    using Dapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Smart.Resolver;

    using WebApplication.Infrastructure.Data;
    using WebApplication.Settings;

    /// <summary>
    ///
    /// </summary>
    public class Startup : IDisposable
    {
        private readonly StandardResolver resolver = new StandardResolver();

        /// <summary>
        ///
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            resolver.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddMvc(options =>
            {
                options.OutputFormatters.RemoveType<JsonOutputFormatter>();
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                options.OutputFormatters.Insert(0, new JsonOutputFormatter(settings, ArrayPool<char>.Shared));
            });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                //options.SingleApiVersion(new Info
                //{
                //    Version = "v1",
                //    Title = "Test API",
                //    Description = "Test ASP.NET Core Web API",
                //    TermsOfService = "None",
                //    Contact = new Contact { Name = "うさ☆うさ", Email = "machi.pon@gmail.com" }
                //});
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var xmlPath = location.Replace("dll", "xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
                options.DescribeAllEnumsAsStrings();    // Enum
            });

            // Replace activator.
            services.AddSingleton<IControllerActivator>(new SmartResolverControllerActivator(resolver));
            services.AddSingleton<IViewComponentActivator>(new SmartResolverViewComponentActivator(resolver));

            // Settings
            ConfigureSettings(services);

            // Add application services.
            SetupComponents();

            // Prepare database
            SetupDatabase();

            return SmartResolverHelper.BuildServiceProvider(resolver, services);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Debug);

            // Error
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Static
            app.UseStaticFiles();

            // MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Swagger
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUi();
            }

            // List controllers
            EnumControllers(app, loggerFactory.CreateLogger<Startup>());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureSettings(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
        }

        /// <summary>
        ///
        /// </summary>
        private void SetupComponents()
        {
            var connectionString = Configuration.GetConnectionString("Test");
            resolver
                .Bind<IConnectionFactory>()
                .ToConstant(new CallbackConnectionFactory(() => new SqliteConnection(connectionString)));
        }

        /// <summary>
        ///
        /// </summary>
        private void SetupDatabase()
        {
            var factory = resolver.Get<IConnectionFactory>();
            using (var con = factory.CreateConnection())
            {
                con.Execute("CREATE TABLE IF NOT EXISTS Data (Id int PRIMARY KEY, Name text)");
                con.Execute("DELETE FROM Data");
                con.Execute("INSERT INTO Data (Id, Name) VALUES (1, 'Data-1')");
                con.Execute("INSERT INTO Data (Id, Name) VALUES (2, 'Data-2')");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        private static void EnumControllers(IApplicationBuilder app, ILogger<Startup> logger)
        {
            var manager = app.ApplicationServices.GetRequiredService<ApplicationPartManager>();

            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);

            logger.LogDebug("[Controllers]");
            foreach (var typeInfo in feature.Controllers)
            {
                logger.LogDebug("Controller : Type=[{0}]", typeInfo.AsType());
            }

            var componentProvider = app.ApplicationServices.GetService<IViewComponentDescriptorProvider>();

            logger.LogDebug("[ViewComponents]");
            foreach (var viewComponent in componentProvider.GetViewComponents())
            {
                logger.LogDebug("ViewComponent : Type=[{0}]", viewComponent.TypeInfo.AsType());
            }
        }
    }
}
