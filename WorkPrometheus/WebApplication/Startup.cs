namespace WebApplication
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Prometheus;

    using Smart.Data;
    using Smart.Data.Accessor.Resolver;
    using Smart.Data.Mapper;
    using Smart.Resolver;

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
            services.AddControllersWithViews();
        }

        public void ConfigureContainer(ResolverConfig config)
        {
            config.UseDataAccessor();
            config.BindSingleton<IDbProvider>(new DelegateDbProvider(() => new SqliteConnection("Data Source=test.db")));

            config.BindSingleton<SmartMetrics>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SmartMetrics metrics, IDbProvider dbProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseHttpMetrics();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapMetrics();
            });

            AppMetrics.AddMetricsToRegistry(Metrics.DefaultRegistry);
            metrics.AddMetricsToRegistry(Metrics.DefaultRegistry);

            dbProvider.Using(con =>
            {
                con.Execute("CREATE TABLE IF NOT EXISTS Data (Id int PRIMARY KEY, Name text, Type text)");
                con.Execute("DELETE FROM Data");
                con.Execute("INSERT INTO Data (Id, Name, Type) VALUES (1, 'Name-1', 'A')");
                con.Execute("INSERT INTO Data (Id, Name, Type) VALUES (2, 'Name-2', 'B')");
                con.Execute("INSERT INTO Data (Id, Name, Type) VALUES (3, 'Name-3', 'A')");
            });
        }
    }
}
