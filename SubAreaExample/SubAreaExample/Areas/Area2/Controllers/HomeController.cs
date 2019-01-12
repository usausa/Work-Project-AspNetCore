namespace SubAreaExample.Areas.Area2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("area2")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
