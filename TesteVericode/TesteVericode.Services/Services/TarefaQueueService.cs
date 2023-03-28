using RabbitMQ.Client;
using TesteVericode.Domain.Entities;
using TesteVericode.Domain.Repositories;
using TesteVericode.Services.Interfaces;

namespace TesteVericode.Services.Services
{
    public class TarefaQueueService : IQueueService<Tarefa>
    {
        private readonly string _queueName = "Tarefa";
        private readonly ITarefaRepository _repository;

        public TarefaQueueService(ITarefaRepository repository)
        {
            _repository = repository;
        }

        public Tarefa RetrieveFromQueue()
        {
            BasicGetResult data;
            using (var connection = this.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    data = channel.BasicGet(_queueName, true);
                }
            }
            return data == null ? null : data.Body.ByteArrayToObject<Tarefa>();
        }

        public void AppendToQueue(Tarefa tarefa)
        {
            using (var connection = this.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    this.CreateQueue(channel);
                    channel.BasicPublish(string.Empty, _queueName, null, client.ObjectToByteArray());
                }
            }
        }

        public async Task<Tarefa> Save(Tarefa tarefa)
        {
            return await this._repository.Add(tarefa);
        }

        private ConnectionFactory GetConnectionFactory()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            return connectionFactory;
        }

        public IConnection CreateConnection()
        {
            return this.GetConnectionFactory().CreateConnection();
        }

        public QueueDeclareOk CreateQueue(IModel channel)
            => channel.QueueDeclare(_queueName, false, false, false, null);
    }
}
