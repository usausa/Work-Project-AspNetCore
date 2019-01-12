namespace SubAreaExample.Areas.Area1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Areas.Area1.Models;

    [Area("area1")]
    public class DataController : Controller
    {
        public IActionResult List()
        {
            return View(new DataViewModel { Text = "Area1" });
        }
    }
}
