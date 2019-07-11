namespace SubAreaExample.Areas.Area2.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DataController : BaseArea2Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
