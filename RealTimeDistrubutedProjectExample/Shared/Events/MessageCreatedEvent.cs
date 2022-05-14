namespace Shared.Events
{
    public class MessageCreatedEvent : IEvent
    {
        public string Message { get; set; }
        public string Email { get; set; }
        public string ConnectionId { get; set; }
    }
}
