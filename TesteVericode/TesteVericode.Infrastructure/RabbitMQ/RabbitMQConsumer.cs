using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TesteVericode.Core.Handlers.Command;
using TesteVericode.Domain;
using TesteVericode.Domain.Entities;
using TesteVericode.Domain.RabbitMQ;

namespace TesteVericode.Infrastructure.RabbitMQ
{
    public class RabbitMQConsumer : IHostedService
    {
        private readonly IUnitOfWork _repository;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly IRabbitMQService _rabbitMQService;

        public RabbitMQConsumer(
            IServiceProvider serviceProvider,
            ILogger<RabbitMQConsumer> logger,
            IRabbitMQService rabbitMQService)
        {
            _repository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IUnitOfWork>();
            _logger = logger;
            _rabbitMQService = rabbitMQService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _rabbitMQService.StartListening(async message =>
                {
                    _logger.LogInformation($"Nova mensagem recebida: {message}");

                    var messageJson = JObject.Parse(message);

                    var messageCommand = JObject.Parse(message)["Command"].Value<string>();

                    await CreateTarefaCommand(messageCommand, message);
                    await UpdateTarefaCommand(messageCommand, message);
                    await DeleteTarefaCommand(messageCommand, message);

                });

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar o serviço!");
                return Task.CompletedTask;
            }
        }

        private async Task CreateTarefaCommand(string command, string message)
        {
            if(command == "CreateTarefaCommand")
            {
                var createTarefaCommand = JsonConvert.DeserializeObject<CreateTarefaCommand>(message);

                if (createTarefaCommand != null)
                {
                    var entity = new Tarefa
                    {
                        Descricao = createTarefaCommand.Model.Descricao,
                        Data = createTarefaCommand.Model.Data,
                        Status = createTarefaCommand.Model.Status
                    };

                    _repository.Tarefas.Add(entity);
                    await _repository.CommitAsync();
                }
            }
        }

        private async Task UpdateTarefaCommand(string command, string message)
        {
            if (command == "UpdateTarefaCommand")
            {
                var updateTarefaCommand = JsonConvert.DeserializeObject<UpdateTarefaCommand>(message);
                var entityToUpdate = _repository.Tarefas.Get(updateTarefaCommand.Id);
                
                if (entityToUpdate != null)
                {
                    entityToUpdate.Descricao = updateTarefaCommand.Model.Descricao;
                    entityToUpdate.Data = updateTarefaCommand.Model.Data;
                    entityToUpdate.Status = updateTarefaCommand.Model.Status;
                    _repository.Tarefas.Update(entityToUpdate);
                    await _repository.CommitAsync();
                }
            }
        }

        private async Task DeleteTarefaCommand(string command, string message)
        {
            if (command == "DeleteTarefaCommand")
            {
                var deleteTarefaCommand = JsonConvert.DeserializeObject<DeleteTarefaCommand>(message);

                _repository.Tarefas.Delete(deleteTarefaCommand.Id);
                await _repository.CommitAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMQ está sendo finalizado...");
            return Task.CompletedTask;
        }
    }
}
