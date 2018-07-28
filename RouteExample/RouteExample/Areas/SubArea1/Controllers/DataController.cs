using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.SubArea1.Controllers
{
    public class DataController : BaseSubArea1Controller
    {
        [Route("~/[area]/[controller]")]
        //[Route("~/[area]/[controller]/[action]", Order = 1)]
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
