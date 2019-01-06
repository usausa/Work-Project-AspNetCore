namespace HealthExample.HealthChecks
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Diagnostics.HealthChecks;

    using Newtonsoft.Json;

    public class DebugPublisher : IHealthCheckPublisher
    {
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(report));

            return Task.CompletedTask;
        }
    }
}
