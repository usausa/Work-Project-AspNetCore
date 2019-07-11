namespace SubAreaExample.Areas.Area1.SubArea2
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;

    [Area("area1")]
    [SubArea("subarea2")]
    [Route("[area]/[subarea]/[controller]/[action]")]
    public abstract class BaseArea2SubArea1Controller : Controller
    {
    }
}
