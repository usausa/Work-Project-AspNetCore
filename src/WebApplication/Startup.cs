namespace WebApplication
{
    using System.Buffers;
    using System.IO;

    using Dapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
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
    using WebApplication.Infrastructure.Resolver;
    using WebApplication.Settings;

    /// <summary>
    ///
    /// </summary>
    public class Startup
    {
        private readonly StandardResolver resolver = new StandardResolver();

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
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
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
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var xmlPath = location.Replace("dll", "xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
                options.DescribeAllEnumsAsStrings();    // Enum
            });

            // UseSmartResolverRequestScope need IHttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(new SmartResolverControllerActivator(resolver));
            services.AddSingleton<IViewComponentActivator>(new SmartResolverViewComponentActivator(resolver));

            services.AddOptions();

            // Setup
            SetupResolver();
            SetupDatabase();
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
            loggerFactory.AddDebug();

            // Enable Smart.Resolver request scope, Placed before UseMvc
            app.UseSmartResolverRequestScope(resolver);

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
        }

        /// <summary>
        ///
        /// </summary>
        private void SetupResolver()
        {
            var connectionString = Configuration.GetConnectionString("Test");
            resolver
                .Bind<IConnectionFactory>()
                .ToConstant(new CallbackConnectionFactory(() => new SqliteConnection(connectionString)));

            var settings = new SmtpSettings();
            Configuration.GetSection("SmtpSettings").Bind(settings);
            resolver
                .Bind<SmtpSettings>()
                .ToConstant(settings);
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
    }
}
