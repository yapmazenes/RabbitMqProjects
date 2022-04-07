using DemandManagement.MessageContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandManagement.Thirdparty.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "ThirdParty Service";

            var bus = BusConfigurator.ConfigureBus((cfg) =>
            {
                cfg.ReceiveEndpoint(RabbitMqConsts.ThirdPartyServiceQueue, e =>
                {
                    e.Consumer<DemandRegisteredEventConsumer>();
                    e.UseRateLimit(1000, TimeSpan.FromMinutes(1));
                });
            });

            bus.StartAsync();
            Console.WriteLine("Listening for Demand Registered events... Press enter to exit");
            Console.ReadLine();
            bus.StopAsync();
        }
    }
}
