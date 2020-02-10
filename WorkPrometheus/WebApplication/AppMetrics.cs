using System;
using Prometheus;

namespace WebApplication
{
    public static class AppMetrics
    {
        public static Counter Sample { get; } =
            Metrics.CreateCounter("app_sample_total", "Sample counter");

        public static Gauge Callback { get; } =
            Metrics.CreateGauge("app_callback_gauge", "Callback gauge");

        public static void AddMetricsToRegistry(CollectorRegistry registry)
        {
            var rand = new Random();

            registry.AddBeforeCollectCallback(() =>
            {
                System.Diagnostics.Debug.WriteLine("**** callback called. ****");
                Callback.Set(rand.NextDouble());
            });
        }
    }
}
