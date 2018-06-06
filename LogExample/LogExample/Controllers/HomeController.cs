namespace LogExample.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using LogExample.Models;

    public class HomeController : Controller
    {
        private ILogger<HomeController> Log { get; }

        public HomeController(ILogger<HomeController> log)
        {
            Log = log;
        }

        public IActionResult Index()
        {
            Log.LogInformation("Info");
            Log.LogWarning("Warning");
            Log.LogError("Error");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
