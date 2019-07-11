namespace SubAreaExample.Infrastructure.Mvc
{
    using Microsoft.AspNetCore.Mvc;

    public sealed class AreaRouteAttribute : RouteAttribute
    {
        public AreaRouteAttribute()
            : base("~/[area]")
        {
        }
    }
}
