namespace SubAreaExample.Areas.Area2.SubArea1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;
    using SubAreaExample.Infrastructure.Mvc;

    public class HomeController : BaseArea2SubArea1Controller
    {
        [SubAreaRoute]
        public IActionResult Index()
        {
            return View();
        }
    }
}
