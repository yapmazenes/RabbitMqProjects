using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagement.MessageContracts;
using ProductManagement.MessageContracts.Consumers;
using ProductManagement.Notification.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Notification.Controllers
{
    public class ProductNotificationController : Controller
    {
        public async Task<IActionResult> Index()
        {

            var bus = BusConfigurator.ConfigureBus(factory =>
            {
                factory.ReceiveEndpoint(RabbitMqConstants.NotificationServiceQueue, endpoint =>
                {
                    endpoint.Consumer<ProductNotificationEventConsumer>();
                });
            });

            await bus.StartAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
