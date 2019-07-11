namespace SubAreaExample.Infrastructure.Mvc
{
    using Microsoft.AspNetCore.Mvc;

    public sealed class SubAreaControllerRouteAttribute : RouteAttribute
    {
        public SubAreaControllerRouteAttribute()
            : base("~/[area]/[subarea]/[controller]")
        {
        }
    }
}
