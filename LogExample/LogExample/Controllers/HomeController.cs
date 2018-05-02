using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogExample.Models;
using Microsoft.Extensions.Logging;

namespace LogExample.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> Log { get; }

        public HomeController(ILogger<HomeController> log)
        {
            Log = log;
        }

        public IActionResult Index()
        {
            Log.LogInformation("Info");
            Log.LogWarning("Warning");
            Log.LogError("Error");

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
