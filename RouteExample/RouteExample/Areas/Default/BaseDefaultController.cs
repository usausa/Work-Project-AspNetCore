using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.Default
{
    [Area("Default")]
    [Route("[controller]/[action]")]
    public class BaseDefaultController : Controller
    {
    }
}
