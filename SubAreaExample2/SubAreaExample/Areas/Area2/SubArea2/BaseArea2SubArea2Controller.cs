namespace SubAreaExample.Areas.Area2.SubArea2
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;

    [Area("area2")]
    [SubArea("subarea2")]
    [Route("[area]/[subarea]/[controller]/[action]")]
    public abstract class BaseArea2SubArea2Controller : Controller
    {
    }
}
