namespace AreaExample.Areas.Default.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Default")]
    public class OtherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
