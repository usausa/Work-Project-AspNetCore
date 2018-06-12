namespace AreaExample.Areas.SubSystem2.Controllers
{
    using AreaExample.Areas.SubSystem2.Models;

    using Microsoft.AspNetCore.Mvc;

    [Area("SubSystem2")]
    public class Home2Controller : Controller
    {
        public IActionResult Index()
        {
            return View(new Home2ViewModel { Condition = true });
        }
    }
}
