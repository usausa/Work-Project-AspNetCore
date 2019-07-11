namespace SubAreaExample.Areas.Area2.SubArea2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;

    public class DataController : BaseArea2SubArea2Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
