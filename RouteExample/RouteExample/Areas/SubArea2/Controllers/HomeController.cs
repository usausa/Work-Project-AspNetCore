using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.SubArea2.Controllers
{
    public class HomeController : BaseSubArea2Controller
    {
        [Route("~/[area]")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
