namespace WebApplication.Infrastructure.Resolver
{
    using System;

    using Microsoft.AspNetCore.Mvc.ViewComponents;

    using Smart.Resolver;

    /// <summary>
    ///
    /// </summary>
    public class SmartResolverViewComponentActivator : IViewComponentActivator
    {
        private readonly IResolver resolver;

        /// <summary>
        ///
        /// </summary>
        /// <param name="resolver"></param>
        public SmartResolverViewComponentActivator(IResolver resolver)
        {
            this.resolver = resolver;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Create(ViewComponentContext context)
        {
            return resolver.Get(context.ViewComponentDescriptor.TypeInfo.AsType());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewComponent"></param>
        public void Release(ViewComponentContext context, object viewComponent)
        {
            (viewComponent as IDisposable)?.Dispose();
        }
    }
}
