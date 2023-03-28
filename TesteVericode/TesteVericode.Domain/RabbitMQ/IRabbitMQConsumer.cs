namespace TesteVericode.Domain.RabbitMQ
{
    public interface IRabbitMQConsumer<T>
    {
        Task ConsumeAsync(T message, CancellationToken cancellationToken);
    }
}
