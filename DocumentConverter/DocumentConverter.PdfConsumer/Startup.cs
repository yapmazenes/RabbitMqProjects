using DocumentConverter.Common.Services;
using DocumentConverter.PdfConsumer.Services.Abstract;
using DocumentConverter.PdfConsumer.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentConverter.PdfConsumer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(sp => new ConnectionFactory
            {
                //Uri = new Uri(Configuration.GetConnectionString("RabbitMQServer"))
                HostName = "localhost"
            })
            .AddSingleton<RabbitMqClientService>()
            .AddSingleton<IEmailService, EmailManager>();
        }
    }
}
