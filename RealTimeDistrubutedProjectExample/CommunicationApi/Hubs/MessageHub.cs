using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommunicationApi.Hubs
{
    public class MessageHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("GetConnectionId", this.Context.ConnectionId);
        }

        public async Task SendMessageAsync(string message, string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("receivemessage", message);
        }
    }
}
