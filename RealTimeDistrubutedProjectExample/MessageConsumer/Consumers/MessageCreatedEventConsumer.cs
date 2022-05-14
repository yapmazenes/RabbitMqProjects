using MassTransit;
using MessageConsumer.Services.Abstract;
using Shared.Events;
using System;
using System.Threading.Tasks;

namespace MessageConsumer.Consumers
{
    public class MessageCreatedEventConsumer : IConsumer<MessageCreatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IHubClientBuilder _hubClientBuilder;

        public MessageCreatedEventConsumer(IEmailService emailService, IHubClientBuilder hubClientBuilder)
        {
            _emailService = emailService;
            _hubClientBuilder = hubClientBuilder;
        }

        public async Task Consume(ConsumeContext<MessageCreatedEvent> context)
        {
            try
            {
                /* await _emailService.EmailSend(new EmailModel
                 {
                     EmailReceiverList = new List<string> { context.Message.Email },
                     Subject = "Message",
                     Body = context.Message.Message,
                     From = "yapmazenes@gmail.com",
                 });
                */
                Console.WriteLine($"{context.Message.Message} sended to {context.Message.Email}");

                await _hubClientBuilder.SendMessageAsync($"{context.Message.Message} sended to {context.Message.Email}", context.Message.ConnectionId);

            }
            catch (Exception e)
            {


            }

        }
    }
}
