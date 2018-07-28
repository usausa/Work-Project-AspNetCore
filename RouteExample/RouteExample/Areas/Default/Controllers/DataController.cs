using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.Default.Controllers
{
    public class DataController : BaseDefaultController
    {
        [Route("~/[controller]")]
        //[Route("~/[controller]/[action]", Order = 1)]
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
