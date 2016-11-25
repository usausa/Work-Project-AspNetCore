namespace WebApplication.Infrastructure.Resolver
{
    using Smart.Resolver;
    using Smart.Resolver.Scopes;

    /// <summary>
    ///
    /// </summary>
    public class RequestScope : IScope
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="kernel"></param>
        /// <returns></returns>
        public IScopeStorage GetStorage(IKernel kernel)
        {
            return kernel.Components.Get<RequestScopeStorage>();
        }
    }
}
