using System.Threading.Tasks;

namespace SignalRExample.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using SignalRExample.Models;
    using SignalRExample.Services;

    public class HomeController : Controller
    {
        public CounterService CounterService { get; }

        public HomeController(CounterService counterService)
        {
            CounterService = counterService;
        }

        public async Task<IActionResult> Index()
        {
            await CounterService.Increment();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
