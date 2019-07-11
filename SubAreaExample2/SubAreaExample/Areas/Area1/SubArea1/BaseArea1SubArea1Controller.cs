namespace SubAreaExample.Areas.Area1.SubArea1
{
    using Microsoft.AspNetCore.Mvc;

    using SubAreaExample.Infrastructure;

    [Area("area1")]
    [SubArea("subarea1")]
    [Route("[area]/[subarea]/[controller]/[action]")]
    public abstract class BaseArea1SubArea1Controller : Controller
    {
    }
}
