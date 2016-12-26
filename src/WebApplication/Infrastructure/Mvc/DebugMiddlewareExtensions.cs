namespace WebApplication.Infrastructure.Mvc
{
    using System;

    using Microsoft.AspNetCore.Builder;

    /// <summary>
    ///
    /// </summary>
    public static class DebugMiddlewareExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDebug(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<DebugMiddleware>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDebug(this IApplicationBuilder app, DebugMiddlewareOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<DebugMiddleware>(options);
        }
    }
}
