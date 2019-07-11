namespace SubAreaExample.Areas.Area1.SubArea2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;
    using SubAreaExample.Infrastructure.Mvc;

    [Area("area1")]
    [SubArea("subarea2")]
    public class HomeController : BaseArea2SubArea1Controller
    {
        [SubAreaRoute]
        public IActionResult Index()
        {
            return View();
        }
    }
}
