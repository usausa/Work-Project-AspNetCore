using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;

namespace TcpServerExample
{
    public class ServiceConnectionHandler : ConnectionHandler
    {
        private ILogger<ServiceConnectionHandler> Log { get; }

        public ServiceConnectionHandler(ILogger<ServiceConnectionHandler> log)
        {
            Log = log;
        }

        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            try
            {
                Log.LogInformation($"Connection start. id=[{connection.ConnectionId}]");

                var cts = new CancellationTokenSource();
                while (true)
                {
                    cts.CancelAfter(5000);
                    var result = await connection.Transport.Input.ReadAsync(cts.Token);
                    var buffer = result.Buffer;

                    Log.LogInformation($"Connection read completed. id=[{connection.ConnectionId}], length=[{buffer.Length}]");

                    foreach (var segment in buffer)
                    {
                        cts.CancelAfter(5000);
                        await connection.Transport.Output.WriteAsync(segment, cts.Token);
                    }

                    if (result.IsCompleted || result.IsCanceled)
                    {
                        break;
                    }

                    connection.Transport.Input.AdvanceTo(buffer.End);
                }
            }
            catch (OperationCanceledException)
            {
                Log.LogWarning($"Connection timeout. id=[{connection.ConnectionId}]");
            }
            catch (Exception e)
            {
                Log.LogError(e, "Unknown exception.");
            }
            finally
            {
                Log.LogInformation($"Connection end. id=[{connection.ConnectionId}]");

                connection.Transport.Input.Complete();
                connection.Transport.Output.Complete();
            }
        }
    }
}
