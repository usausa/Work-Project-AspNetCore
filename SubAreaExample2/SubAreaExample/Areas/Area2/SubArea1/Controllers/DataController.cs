namespace SubAreaExample.Areas.Area2.SubArea1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;

    public class DataController : BaseArea2SubArea1Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
