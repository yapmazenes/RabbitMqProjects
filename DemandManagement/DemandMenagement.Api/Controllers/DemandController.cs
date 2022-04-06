using DemandManagement.MessageContracts;
using DemandMenagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DemandMenagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterDemandModel registerDemandModel)
        {
            var bus = BusConfigurator.ConfigureBus();

            var sendToUri = new Uri($"{RabbitMqConsts.RabbitMqUri}{RabbitMqConsts.RegisterDemandServiceQueue}");

            var endpoint = await bus.GetSendEndpoint(sendToUri);

            await endpoint.Send<IRegisterDemandCommand>(registerDemandModel);

            return Ok();
        }
    }
}
