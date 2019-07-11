namespace SubAreaExample.Infrastructure.Mvc
{
    using Microsoft.AspNetCore.Mvc;

    public sealed class SubAreaRouteAttribute : RouteAttribute
    {
        public SubAreaRouteAttribute()
            : base("~/[area]/[subarea]")
        {
        }
    }
}
