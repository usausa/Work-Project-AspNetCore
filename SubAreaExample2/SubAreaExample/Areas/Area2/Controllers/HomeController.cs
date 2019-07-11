namespace SubAreaExample.Areas.Area2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure.Mvc;

    public class HomeController : BaseArea2Controller
    {
        [AreaRoute]
        public IActionResult Index()
        {
            return View();
        }
    }
}
