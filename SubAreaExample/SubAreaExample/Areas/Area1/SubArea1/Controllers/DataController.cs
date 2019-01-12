namespace SubAreaExample.Areas.Area1.SubArea1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Areas.Area1.SubArea1.Models;
    using SubAreaExample.Infrastructure;

    [Area("area1")]
    [SubArea("subarea1")]
    public class DataController : Controller
    {
        public IActionResult List()
        {
            return View(new DataViewModel { Text = "Area1/SubArea1" });
        }
    }
}
