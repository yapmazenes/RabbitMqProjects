using MassTransit;
using ProductManagement.MessageContracts;
using ProductManagement.MessageContracts.Consumers;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Registration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus(factory =>
            {
                factory.ReceiveEndpoint(RabbitMqConstants.RegistrationServiceQueue, endpoint =>
                {
                    endpoint.Consumer<ProductRegistrationCommandConsumer>();
                });
            });

            await bus.StartAsync();
            await Task.Run(() => Console.ReadLine());
            await bus.StopAsync();
        }
    }
}
