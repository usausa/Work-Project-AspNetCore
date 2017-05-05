namespace Application.Web.Mvc.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///
    /// </summary>
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    public abstract class BaseMvcController : Controller
    {
    }
}
