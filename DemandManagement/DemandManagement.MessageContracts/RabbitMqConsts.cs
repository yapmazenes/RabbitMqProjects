using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandManagement.MessageContracts
{
    public class RabbitMqConsts
    {
        public const string RabbitMqUri = "rabbitmq://localhost/demand/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegisterDemandServiceQueue = "registerdemand.service";
        public const string NotificationServiceQueue = "notification.service";
        public const string ThirdPartyServiceQueue = "thirdparty.service";

    }
}
