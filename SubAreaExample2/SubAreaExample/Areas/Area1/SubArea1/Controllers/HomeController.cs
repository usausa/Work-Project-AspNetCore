namespace SubAreaExample.Areas.Area1.SubArea1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;
    using SubAreaExample.Infrastructure.Mvc;

    [Area("area1")]
    [SubArea("subarea1")]
    public class HomeController : BaseArea1SubArea1Controller
    {
        [SubAreaRoute]
        public IActionResult Index()
        {
            return View();
        }
    }
}
