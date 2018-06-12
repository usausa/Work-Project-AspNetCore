namespace AreaExample.Areas.SubSystem1.Controllers
{
    using AreaExample.Areas.SubSystem1.Models;

    using Microsoft.AspNetCore.Mvc;

    [Area("SubSystem1")]
    public class Home1Controller : Controller
    {
        public IActionResult Index()
        {
            return View(new Home1ViewModel { Condition = true });
        }
    }
}
