namespace SubAreaExample.Areas.Area1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Areas.Area1.Models;

    public class DataController : BaseArea1Controller
    {
        public IActionResult List()
        {
            return View(new DataViewModel { Text = "Area1" });
        }
    }
}
