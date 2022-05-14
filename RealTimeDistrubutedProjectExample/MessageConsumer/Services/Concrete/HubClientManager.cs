using MessageConsumer.Services.Abstract;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace MessageConsumer.Services.Concrete
{
    public class HubClientManager : IHubClientBuilder
    {
        public HubConnection _hubConnection;
        public HubClientManager()
        {
            if (_hubConnection == null)
            {
                _hubConnection = GetHubConnection();
            }
        }

        public HubConnection GetHubConnection() => new HubConnectionBuilder().WithUrl("https://localhost:44306/messagehub")
                                                           .Build();



        public async Task SendMessageAsync(string message, string connectionId)
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }

            await _hubConnection.InvokeAsync("SendMessageAsync", message, connectionId);
        }

    }
}
