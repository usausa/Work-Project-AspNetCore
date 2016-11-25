namespace WebApplication.Infrastructure.Resolver
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;

    using Smart.Resolver;

    /// <summary>
    ///
    /// </summary>
    public class SmartResolverControllerActivator : IControllerActivator
    {
        private readonly IResolver resolver;

        /// <summary>
        ///
        /// </summary>
        /// <param name="resolver"></param>
        public SmartResolverControllerActivator(IResolver resolver)
        {
            this.resolver = resolver;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Create(ControllerContext context)
        {
            return resolver.Get(context.ActionDescriptor.ControllerTypeInfo.AsType());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="controller"></param>
        public void Release(ControllerContext context, object controller)
        {
            (controller as IDisposable)?.Dispose();
        }
    }
}
