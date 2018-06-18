namespace SignalRExample.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SignalRExample.Models;
    using SignalRExample.Services;

    public class DashboardController : Controller
    {
        public CounterService CounterService { get; }

        public DashboardController(CounterService counterService)
        {
            CounterService = counterService;
        }

        public IActionResult Index()
        {
            return View(new DashboardViewModel
            {
                Counter = CounterService.Query()
            });
        }
    }
}
