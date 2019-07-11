namespace SubAreaExample.Areas.Area1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure.Mvc;

    public class HomeController : BaseArea1Controller
    {
        [AreaRoute]
        public IActionResult Index()
        {
            return View();
        }
    }
}
