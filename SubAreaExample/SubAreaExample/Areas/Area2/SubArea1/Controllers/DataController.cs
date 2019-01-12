namespace SubAreaExample.Areas.Area2.SubArea1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;

    [Area("area2")]
    [SubArea("subarea1")]
    public class DataController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
