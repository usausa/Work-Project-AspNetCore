namespace SubAreaExample.Areas.Area2.SubArea2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;

    [Area("area2")]
    [SubArea("subarea2")]
    public class DataController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
