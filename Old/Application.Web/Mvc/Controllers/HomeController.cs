namespace Application.Web.Mvc.Controllers
{
    using Application.Web.Mvc.Infrastructure;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class HomeController : BaseMvcController
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogDebug("Debug.");
            logger.LogInformation("Information.");

            return View();
        }
    }
}
