namespace SubAreaExample.Areas.Area2.SubArea2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;
    using SubAreaExample.Infrastructure.Mvc;

    public class HomeController : BaseArea2SubArea2Controller
    {
        [SubAreaRoute]
        public IActionResult Index()
        {
            return View();
        }
    }
}
