namespace SubAreaExample.Areas.Area2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("area2")]
    public class DataController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
