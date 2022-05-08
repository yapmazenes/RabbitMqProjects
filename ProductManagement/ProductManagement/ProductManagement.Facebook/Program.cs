using MassTransit;
using ProductManagement.MessageContracts;
using ProductManagement.MessageContracts.Commands;
using ProductManagement.MessageContracts.Consumers;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Facebook
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus(factory =>
            {
                factory.ReceiveEndpoint(RabbitMqConstants.InstagramServiceQueue, endpoint =>
                {
                    endpoint.Consumer<ProductFacebookEventConsumer>();
                });
            });

            await bus.StartAsync();
            await Task.Run(() => Console.ReadLine());
            await bus.StopAsync();
        }
    }
}
