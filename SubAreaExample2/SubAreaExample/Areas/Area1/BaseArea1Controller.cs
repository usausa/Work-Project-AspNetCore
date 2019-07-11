namespace SubAreaExample.Areas.Area1
{
    using Microsoft.AspNetCore.Mvc;

    [Area("area1")]
    [Route("[area]/[controller]/[action]")]
    public abstract class BaseArea1Controller : Controller
    {
    }
}
