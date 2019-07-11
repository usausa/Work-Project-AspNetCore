namespace SubAreaExample.Infrastructure.Mvc
{
    using Microsoft.AspNetCore.Mvc;

    public sealed class DefaultControllerRouteAttribute : RouteAttribute
    {
        public DefaultControllerRouteAttribute()
            : base("~/[controller]")
        {
        }
    }
}
