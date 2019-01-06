namespace HostExample
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class BatchService : IHostedService
    {
        private ILogger<BatchService> Log { get; }

        private IApplicationLifetime ApplicationLifetime { get; }

        private BatchSettings Settings { get; }

        public BatchService(
            ILogger<BatchService> log,
            IApplicationLifetime applicationLifetime,
            IOptions<BatchSettings> settings)
        {
            Log = log;
            ApplicationLifetime = applicationLifetime;
            Settings = settings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.LogInformation("StartAsync");

            ApplicationLifetime.ApplicationStarted.Register(Run);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Log.LogInformation("StopAsync");

            return Task.CompletedTask;
        }

        private async void Run()
        {
            Log.LogInformation($"Started. value=[{Settings.Value}]");

            try
            {
                // TODO
                await Task.Delay(30000);
            }
            catch (Exception e)
            {
                Log.LogError(e, "Error");
            }
            finally
            {
                ApplicationLifetime.StopApplication();
            }
        }

    }
}
