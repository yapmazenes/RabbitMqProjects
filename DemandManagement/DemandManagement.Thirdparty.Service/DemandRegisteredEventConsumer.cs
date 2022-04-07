using DemandManagement.MessageContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandManagement.Thirdparty.Service
{
    public class DemandRegisteredEventConsumer : IConsumer<IRegisteredDemandEvent>
    {
        public async Task Consume(ConsumeContext<IRegisteredDemandEvent> context)
        {
            await Console.Out.WriteLineAsync($"ThirdParty integration done: Demand id {context.Message.DemandId}, Time: {DateTime.Now}");

        }
    }
}
