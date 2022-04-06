﻿using MassTransit;
using System;

namespace DemandManagement.MessageContracts
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(RabbitMqConsts.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqConsts.UserName);
                    hst.Username(RabbitMqConsts.Password);
                });

                registrationAction?.Invoke(cfg);
            });
        }


    }
}
