using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prometheus;

namespace WebApplication
{
    public static class Counters
    {
        public static Counter Sample { get; } =
            Metrics.CreateCounter("app_sample_total", "Sample counter");

        public static Gauge Callback { get; } =
            Metrics.CreateGauge("app_callback_gauge", "Callback gauge");

        public static void SetupCallback()
        {
            var rand = new Random();

            Metrics.DefaultRegistry.AddBeforeCollectCallback(() =>
            {
                System.Diagnostics.Debug.WriteLine("**** callback called. ****");
                Callback.Set(rand.NextDouble());
            });
        }
    }
}
