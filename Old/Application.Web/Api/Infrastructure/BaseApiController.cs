namespace Application.Web.Api.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;

    [Area("api")]
    [Route("[area]/[controller]/[action]")]
    public class BaseApiController : Controller
    {
    }
}
