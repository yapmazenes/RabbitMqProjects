using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;
using Shared.Interfaces;
using System.Threading.Tasks;

namespace CommunicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] MessageCreatedEvent userModel)
        {
            await _publishEndpoint.Publish(userModel);
            return Ok(true);
        }
    }
}
