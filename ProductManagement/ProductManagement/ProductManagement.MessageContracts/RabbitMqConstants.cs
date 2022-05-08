using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.MessageContracts
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost";
        public const string Username = "guest";
        public const string Password = "guest";
        public const string RegistrationServiceQueue = "registration.service";
        public const string NotificationServiceQueue = "notification.service";
        public const string FacebookServiceQueue = "facebook.service";
        public const string InstagramServiceQueue = "instagram.service";
    }
}
