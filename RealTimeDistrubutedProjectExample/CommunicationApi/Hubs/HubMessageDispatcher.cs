using Microsoft.AspNetCore.SignalR;
using Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace CommunicationApi.Hubs
{
    public class HubMessageDispatcher : IHubMessageDispatcher
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public HubMessageDispatcher(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(string message, string connectionId)
        {
            try
            {
                await _hubContext.Clients.User(connectionId).SendAsync("receivemessage", message);
            }
            catch (Exception e)
            {
            }
        }
    }
}
