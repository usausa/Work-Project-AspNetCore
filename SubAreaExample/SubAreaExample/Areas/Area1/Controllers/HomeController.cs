namespace SubAreaExample.Areas.Area1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("area1")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
