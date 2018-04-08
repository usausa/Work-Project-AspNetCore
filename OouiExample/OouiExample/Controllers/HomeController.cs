namespace OouiExample.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using Ooui.AspNetCore;
    using OouiExample.Models;
    using OouiExample.Pages;

    using Xamarin.Forms;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var page = new HomePage();
            var element = page.GetOouiElement();
            return new ElementResult(element, "Home page");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
