using RabbitMQ.Client;

namespace TesteVericode.Domain.RabbitMQ
{
    public interface IRabbitMQService
    {
        void SendMessage<TMessage>(TMessage message);

        IModel GetChannel();

        void StartListening(Action<string> messageHandler);

    }
}
