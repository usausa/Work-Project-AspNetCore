namespace WebApplication.Mvc
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View();
        }
    }
}
