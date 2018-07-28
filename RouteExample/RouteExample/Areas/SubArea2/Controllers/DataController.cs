using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.SubArea2.Controllers
{
    public class DataController : BaseSubArea2Controller
    {
        [Route("~/[area]/[controller]")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Edit(string id)
        {
            return View();
        }

        [HttpGet("{year?}/{month?}/{day?}")]
        public IActionResult Date(int? year, int? month, int? day)
        {
            return View();
        }
    }
}
