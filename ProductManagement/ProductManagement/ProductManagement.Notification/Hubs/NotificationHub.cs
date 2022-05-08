using Microsoft.AspNetCore.SignalR;
using ProductManagement.MessageContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Notification.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessageAsync(Product product)
        {
            await Clients.All.SendAsync("receiveProductNotification", product);
        }
    }
}
