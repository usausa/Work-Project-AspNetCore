using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApplication.Infrastructure.Mvc
{
    /// <summary>
    ///
    /// </summary>
    public class DebugMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger logger;

        private readonly DebugMiddlewareOptions options;

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        public DebugMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            IOptions<DebugMiddlewareOptions> options)
        {
            this.next = next;
            this.options = options.Value;
            logger = loggerFactory.CreateLogger<DebugMiddleware>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (options.Enable)
            {
                logger.LogDebug("Path=[{0}]", context.Request.Path);
            }

            await next(context);
        }
    }
}
