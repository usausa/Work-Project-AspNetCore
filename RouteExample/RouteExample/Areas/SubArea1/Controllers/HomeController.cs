using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.SubArea1.Controllers
{
    public class HomeController : BaseSubArea1Controller
    {
        [Route("~/[area]")]
        //[Route("~/[area]/[controller]", Order = 1)]
        //[Route("~/[area]/[controller]/[action]", Order = 1)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
