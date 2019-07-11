namespace SubAreaExample.Areas.Default.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Models;
    using SubAreaExample.Infrastructure.Mvc;

    public class HomeController : BaseDefaultController
    {
        [DefaultRoute]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
