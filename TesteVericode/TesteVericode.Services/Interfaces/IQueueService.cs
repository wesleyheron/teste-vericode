using RabbitMQ.Client;

namespace TesteVericode.Services.Interfaces
{
    public interface IQueueService<T>
    {
        void AppendToQueue(T entity);
        T RetrieveFromQueue();
        IConnection CreateConnection();
        Task<T> Save(T entity);
        QueueDeclareOk CreateQueue(IModel channel);
    }
}
