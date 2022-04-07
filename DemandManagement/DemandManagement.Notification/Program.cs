using DemandManagement.MessageContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandManagement.Notification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Notification";

            var bus = BusConfigurator.ConfigureBus((cfg) =>
            {
                cfg.ReceiveEndpoint(RabbitMqConsts.NotificationServiceQueue, e =>
                {
                    e.Consumer<DemandRegisteredEventConsumer>();
                    e.UseMessageRetry(r => r.Immediate(5));
                });
            });

            bus.StartAsync();
            Console.WriteLine("Listening for Demand Registered events... Press enter to exit");
            Console.ReadLine();
            bus.StopAsync();
        }
    }
}
