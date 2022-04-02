using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentConverter.Common.Services
{
    public class RabbitMqClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public static string ExchangeName = "convert-exchange";
        public static string RoutingName = "WordToPdf";
        public static string QueueName = "File";

        public RabbitMqClientService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            Connect();
        }

        public void SetupDirectionNames(string exchangeName, string routingName, string queueName)
        {
            ExchangeName = exchangeName;
            RoutingName = routingName;
            QueueName = queueName;
        }

        public IModel Connect(bool queueDeclare = false)
        {
            try
            {
                if (_connection == null || (_connection is { IsOpen: false }))
                {
                    _connection = _connectionFactory.CreateConnection();
                }

                if (_channel is { IsOpen: true })
                {
                    return _channel;
                }

                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(ExchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false);

                if (queueDeclare)
                    _channel.QueueDeclare(QueueName, true, false, false, null);

                _channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingName);

            }
            catch (Exception e)
            {

            }

            return _channel;
        }

        public void SendMessage(byte[] body, bool persistMessagePayload = false)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = persistMessagePayload;

            _channel.BasicPublish(ExchangeName, RoutingName, properties, body);
        }

        public void ConsumeQueue(EventHandler<BasicDeliverEventArgs> eventHandler, bool autoAck = false, bool basicQos = false)
        {
            if (basicQos)
                _channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(QueueName, autoAck, consumer);

            consumer.Received += eventHandler;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
