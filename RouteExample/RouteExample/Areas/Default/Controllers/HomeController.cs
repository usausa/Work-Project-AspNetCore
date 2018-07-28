using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.Default.Controllers
{
    public class HomeController : BaseDefaultController
    {
        [Route("~/")]
        //[Route("~/[controller]", Order = 1)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
