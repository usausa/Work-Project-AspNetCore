namespace SubAreaExample.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Razor;

    public sealed class SubAreaViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.ActionContext.RouteData.Values.ContainsKey("subarea"))
            {
                var subArea = RazorViewEngine.GetNormalizedRouteValue(context.ActionContext, "subarea");
                return viewLocations.Prepend("/Areas/{2}/" + subArea + "/Views/{1}/{0}.cshtml");
            }

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.ActionContext.ActionDescriptor.RouteValues.TryGetValue("subarea", out var subArea);
            context.Values["subarea"] = subArea;
        }
    }
}
