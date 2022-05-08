using MassTransit;
using Microsoft.AspNetCore.SignalR.Client;
using ProductManagement.MessageContracts.Events;
using ProductManagement.MessageContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.MessageContracts.Consumers
{
    public class ProductNotificationEventConsumer : IConsumer<IProductEvent>
    {
        public async Task Consume(ConsumeContext<IProductEvent> context)
        {
            Console.WriteLine($"{context.Message.ProductName} isimli ürün yayınlanarak müşteriler bilgilendirilmiştir.");
            HubConnection connection = new HubConnectionBuilder().WithUrl("https://localhost:5003/notificationhub").Build();
            await connection.StartAsync();

            await connection.InvokeAsync("SendMessageAsync", context.Message);

            await connection.StopAsync();
        }
    }
}
