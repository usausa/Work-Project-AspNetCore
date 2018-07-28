using Microsoft.AspNetCore.Mvc;

namespace RouteExample.Areas.SubArea1
{
    [Area("SubArea1")]
    [Route("[area]/[controller]/[action]")]
    public class BaseSubArea1Controller : Controller
    {
    }
}
