namespace SignalRExample.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Connected", Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("Disconnected", Context.ConnectionId);
        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Message", Context.ConnectionId, message);
        }
    }
}
