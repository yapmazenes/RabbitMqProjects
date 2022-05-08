using MassTransit;
using ProductManagement.MessageContracts;
using ProductManagement.MessageContracts.Consumers;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Instagram
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus(factory =>
            {
                factory.ReceiveEndpoint(RabbitMqConstants.FacebookServiceQueue, endpoint =>
                {
                    endpoint.Consumer<ProductInstagramEventConsumer>();
                });
            });

            await bus.StartAsync();
            await Task.Run(() => Console.ReadLine());
            await bus.StopAsync();
        }
    }
}
