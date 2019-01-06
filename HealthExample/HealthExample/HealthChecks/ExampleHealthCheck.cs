namespace HealthExample.HealthChecks
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Diagnostics.HealthChecks;

    public class ExampleHealthCheck : IHealthCheck
    {
        // Can DI
        public ExampleHealthCheck()
        {
            Debug.WriteLine("ExampleHealthCheck()");
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var now = DateTime.Now;
            if (now.Second % 2 == 0)
            {
                return Task.FromResult(HealthCheckResult.Healthy("healthy"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("unhealthy"));
        }
    }
}
