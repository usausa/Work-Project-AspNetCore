namespace SubAreaExample.Infrastructure.Mvc
{
    using Microsoft.AspNetCore.Mvc;

    public sealed class AreaControllerRouteAttribute : RouteAttribute
    {
        public AreaControllerRouteAttribute()
            : base("~/[area]/[controller]")
        {
        }
    }
}
