using System.Threading.Tasks;

namespace SignalRExample.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    using SignalRExample.Services;

    public class DashboardHub : Hub
    {
        private CounterService CounterService { get; }

        public DashboardHub(CounterService counterService)
        {
            CounterService = counterService;
        }

        public int Query()
        {
            return CounterService.Query();
        }
    }

    public class DashboardNotifier : ICounterNotifier
    {
        private readonly IHubContext<DashboardHub> hubContext;

        public DashboardNotifier(IHubContext<DashboardHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public Task Update()
        {
            return hubContext.Clients.All.SendAsync("Update", null);
        }
    }

}
