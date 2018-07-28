using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.SubArea2
{
    [Area("SubArea2")]
    [Route("[area]/[controller]/[action]")]
    public class BaseSubArea2Controller : Controller
    {
    }
}
