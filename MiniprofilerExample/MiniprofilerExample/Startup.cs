namespace MiniprofilerExample
{
    using Dapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Smart.Data;

    using StackExchange.Profiling;
    using StackExchange.Profiling.Data;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly IHostingEnvironment env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults
                // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
                //options.RouteBasePath = "/profiler";
                //// (Optional) Control storage
                //// (default is 30 minutes in MemoryCacheStorage)
                //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
                //// (Optional) Control which SQL formatter to use, InlineFormatter is the default
                //options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
                //// (Optional) To control authorization, you can use the Func<HttpRequest, bool> options:
                //// (default is everyone can access profilers)
                //options.ResultsAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;
                //options.ResultsListAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;
                //// (Optional)  To control which requests are profiled, use the Func<HttpRequest, bool> option:
                //// (default is everything should be profiled)
                //options.ShouldProfile = request => MyShouldThisBeProfiledFunction(request);
                //// (Optional) Profiles are stored under a user ID, function to get it:
                //// (default is null, since above methods don't use it by default)
                //options.UserIdProvider = request => MyGetUserIdFunction(request);
                //// (Optional) Swap out the entire profiler provider, if you want
                //// (default handles async and works fine for almost all appliations)
                //options.ProfilerProvider = new MyProfilerProvider();
                // (Optional) You can disable "Connection Open()", "Connection Close()" (and async variant) tracking.
                // (defaults to true, and connection opening/closing is tracked)
                //options.TrackConnectionOpenClose = true;
            });

            if (env.IsDevelopment())
            {
                services.AddSingleton<IConnectionFactory>(new CallbackConnectionFactory(() =>
                    new ProfiledDbConnection(new SqliteConnection("Filename=test.db"), MiniProfiler.Current)));
            }
            else
            {
                services.AddSingleton<IConnectionFactory>(new CallbackConnectionFactory(() => new SqliteConnection("Filename=test.db")));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
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

            if (env.IsDevelopment())
            {
                app.UseMiniProfiler();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            SetupDatabase(app.ApplicationServices.GetService<IConnectionFactory>());
        }

        private void SetupDatabase(IConnectionFactory connectionFactory)
        {
            connectionFactory.Using(con =>
            {
                con.Execute("CREATE TABLE IF NOT EXISTS Data (Id int PRIMARY KEY, Name text, Value int)");
                con.Execute("DELETE FROM Data");
                con.Execute("INSERT INTO Data (Id, Name, Value) VALUES (1, 'Name-1', 100)");
                con.Execute("INSERT INTO Data (Id, Name, Value) VALUES (2, 'Name-2', 200)");
                con.Execute("INSERT INTO Data (Id, Name, Value) VALUES (3, 'Name-3', 300)");
            });
        }
    }
}
