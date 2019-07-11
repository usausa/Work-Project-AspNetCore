namespace SubAreaExample.Areas.Area1.SubArea2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Areas.Area1.SubArea2.Models;
    using SubAreaExample.Infrastructure;

    [Area("area1")]
    [SubArea("subarea2")]
    public class DataController : BaseArea2SubArea1Controller
    {
        public IActionResult List()
        {
            return View(new DataViewModel { Text = "Area1/SubArea2" });
        }
    }
}
