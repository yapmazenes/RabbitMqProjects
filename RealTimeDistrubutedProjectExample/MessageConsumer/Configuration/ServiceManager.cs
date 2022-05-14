using MassTransit;
using MessageConsumer.Consumers;
using MessageConsumer.Services.Abstract;
using MessageConsumer.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;

namespace MessageConsumer.Configuration
{
    public static class ServiceManager
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<IEmailService, EmailManager>();
            services.AddSingleton<IHubClientBuilder, HubClientManager>();
            
            services.AddMassTransit(mt =>
            {
                mt.AddConsumer<MessageCreatedEventConsumer>();

                mt.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(configuration.GetConnectionString("RabbitMQ"));
                    configurator.UseConcurrencyLimit(1);

                    configurator.ReceiveEndpoint(RabbitMQSettings.MessageConsumer_MessageCreatedEventQueue,
                        e =>
                        {
                            e.PrefetchCount = 1;
                            e.UseRetry(retryConfig =>
                            {
                                retryConfig.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));
                            });
                            e.ConfigureConsumer<MessageCreatedEventConsumer>(context);
                        });
                });
            });
        }
    }
}
