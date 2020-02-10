using Smart.Data.Accessor;

namespace WebApplication
{
    using Prometheus;

    using Smart.Resolver;
    using Smart.Data.Accessor.Engine;

    public class SmartMetrics
    {
        private static readonly Gauge ResolverFactoryCacheCount =
            Metrics.CreateGauge("smart_resolver_factory_cache_count", "Smart resolver factory cache count");

        private static readonly Gauge ResolverFactoryCacheWidth =
            Metrics.CreateGauge("smart_resolver_factory_cache_width", "Smart resolver factory cache width");

        private static readonly Gauge ResolverFactoryCacheDepth =
            Metrics.CreateGauge("smart_resolver_factory_cache_depth", "Smart resolver factory cache depth");

        private static readonly Gauge AccessorMapperCacheCount =
            Metrics.CreateGauge("smart_accessor_mapper_cache_count", "Smart accessor mapper cache count");

        private static readonly Gauge AccessorMapperCacheWidth =
            Metrics.CreateGauge("smart_accessor_mapper_cache_width", "Smart accessor mapper cache width");

        private static readonly Gauge AccessorMapperCacheDepth =
            Metrics.CreateGauge("smart_accessor_mapper_cache_depth", "Smart accessor mapper cache depth");


        private readonly SmartResolver resolver;

        private readonly IEngineController engineController;

        public SmartMetrics(SmartResolver resolver, ExecuteEngine executeEngine)
        {
            this.resolver = resolver;
            this.engineController = executeEngine;
        }

        public void AddMetricsToRegistry(CollectorRegistry registry)
        {
            Metrics.DefaultRegistry.AddBeforeCollectCallback(() =>
            {
                var resolverDiagnostics = resolver.Diagnostics;
                ResolverFactoryCacheCount.Set(resolverDiagnostics.FactoryCacheCount);
                ResolverFactoryCacheWidth.Set(resolverDiagnostics.FactoryCacheWidth);
                ResolverFactoryCacheDepth.Set(resolverDiagnostics.FactoryCacheDepth);

                var controllerDiagnostics = engineController.Diagnostics;
                AccessorMapperCacheCount.Set(controllerDiagnostics.ResultMapperCacheCount);
                AccessorMapperCacheWidth.Set(controllerDiagnostics.ResultMapperCacheWidth);
                AccessorMapperCacheDepth.Set(controllerDiagnostics.ResultMapperCacheDepth);
            });
        }
    }
}
