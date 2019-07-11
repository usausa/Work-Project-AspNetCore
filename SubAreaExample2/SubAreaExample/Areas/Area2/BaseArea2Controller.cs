namespace SubAreaExample.Areas.Area2
{
    using Microsoft.AspNetCore.Mvc;

    [Area("area2")]
    [Route("[area]/[controller]/[action]")]
    public abstract class BaseArea2Controller : Controller
    {
    }
}
