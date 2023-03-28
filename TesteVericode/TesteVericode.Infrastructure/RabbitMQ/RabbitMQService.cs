using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TesteVericode.Domain.RabbitMQ;

namespace TesteVericode.Infrastructure.RabbitMQ
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IMediator _mediator;
        private IModel _channel;
        private IConnection _connection;

        public RabbitMQService(
            IMediator mediator,
            IConnection connection,
            IModel channel
            )
        {
            _mediator = mediator;
            _connection = connection;
            _channel = channel;

        }

        public void SendMessage<TMessage>(TMessage message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            _channel.BasicPublish(exchange: string.Empty, routingKey: _channel.CurrentQueue, basicProperties: null, body: body);
        }

        public IModel GetChannel()
        {
            return _channel;
        }

        public void StartListening(Action<string> messageHandler)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                messageHandler(message);
            };
            _channel.BasicConsume(queue: _channel.CurrentQueue, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }

}
