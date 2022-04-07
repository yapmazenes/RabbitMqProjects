using DemandManagement.MessageContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandManagement.Registration
{
    public class RegisterDemandCommandConsumer : IConsumer<IRegisterDemandCommand>
    {
        public async Task Consume(ConsumeContext<IRegisterDemandCommand> context)
        {
            var message = context.Message;
            var guid = Guid.NewGuid();
            Console.WriteLine($"Demand successfully registered. Subject: {message.Subject}, Description: {message.Description}, Id:{guid}, Time: {DateTime.Now}");

            await Task.Delay(1000);

            await context.Publish<IRegisteredDemandEvent>(new { DemandId = guid });
        }
    }
}
