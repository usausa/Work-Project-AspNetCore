namespace SubAreaExample.Areas.Default
{
    using Microsoft.AspNetCore.Mvc;

    [Area("default")]
    [Route("[controller]/[action]")]
    public abstract class BaseDefaultController : Controller
    {
    }
}
