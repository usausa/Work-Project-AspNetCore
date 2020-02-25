namespace Application.Web.Mvc.Controllers
{
    using Application.Web.Mvc.Infrastructure;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class HomeController : BaseMvcController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
