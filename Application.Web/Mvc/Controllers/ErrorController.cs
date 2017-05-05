namespace Application.Web.Mvc.Controllers
{
    using Application.Web.Mvc.Infrastructure;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class ErrorController : BaseMvcController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
