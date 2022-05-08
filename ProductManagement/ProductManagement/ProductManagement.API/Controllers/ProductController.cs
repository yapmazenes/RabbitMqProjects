using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.MessageContracts;
using ProductManagement.MessageContracts.Commands;
using ProductManagement.MessageContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            //Ürün karşılanır.
            //Gerekli ön çalışmalar yapılır.

            var bus = BusConfigurator.ConfigureBus();
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}/{RabbitMqConstants.RegistrationServiceQueue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            await endPoint.Send<IProductRegistrationCommand>(product);
            return Ok($"Gönderdiğiniz {product.ProductName} adlı ürün tarafımıza ulaşmış ve gerekli işlemler gerçekleştirilmiştir. İlginiz için teşekkür ederiz.");
        }
    }
}
