using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.Default.Controllers
{
    public class ErrorController : BaseDefaultController
    {
        [Route("~/[controller]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
