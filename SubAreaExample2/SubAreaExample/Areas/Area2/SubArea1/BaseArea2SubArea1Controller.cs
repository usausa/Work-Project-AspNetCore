namespace SubAreaExample.Areas.Area2.SubArea1
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;

    [Area("area2")]
    [SubArea("subarea1")]
    [Route("[area]/[subarea]/[controller]/[action]")]
    public abstract class BaseArea2SubArea1Controller : Controller
    {
    }
}
