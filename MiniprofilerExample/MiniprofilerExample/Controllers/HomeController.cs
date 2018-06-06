namespace MiniprofilerExample.Controllers
{
    using System.Diagnostics;
    using System.Threading;

    using Dapper;

    using Microsoft.AspNetCore.Mvc;

    using MiniprofilerExample.Models;

    using Smart.Data;

    using StackExchange.Profiling;

    public class HomeController : Controller
    {
        private IConnectionFactory ConnectionFactory { get; }

        public HomeController(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public IActionResult Index()
        {
            ConnectionFactory.Using(con => con.ExecuteScalar<int>("SELECT COUNT(*) FROM Data"));

            using (MiniProfiler.Current.Step("Example Step"))
            {
                Thread.Sleep(10);

                using (MiniProfiler.Current.Step("Sub timing"))
                {
                    Thread.Sleep(5);
                }

                using (MiniProfiler.Current.Step("Sub timing 2"))
                {
                    Thread.Sleep(5);
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
