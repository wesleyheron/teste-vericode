using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TesteVericode.Core;
using TesteVericode.Domain;
using TesteVericode.Domain.Entities;
using TesteVericode.Domain.RabbitMQ;
using TesteVericode.Infrastructure;

namespace TesteVericode.Consumer 
{
    public class CreateTarefaMessageConsumer
    {
        //private readonly IMediator _mediator;
        public static IConfigurationRoot configuration;
        private static IRabbitMQService _rabbitMQService;
        private static IUnitOfWork _repository;        

        public CreateTarefaMessageConsumer(
            //IMediator mediator,
            IRabbitMQService rabbitMQService,
            IUnitOfWork repository)
        {
            //_mediator = mediator;
            _rabbitMQService = rabbitMQService;
            _repository = repository;
        }

        static Task Main(string[] args)
        {
            try
            {
                ServiceCollection serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                _rabbitMQService.StartListening(async message =>
                {
                    //_logger.LogInformation($"Nova mensagem recebida: {message}");
                    var tarefa = JsonConvert.DeserializeObject<Tarefa>(message);

                    if (tarefa != null)
                    {
                        _repository.Tarefas.Add(tarefa);
                        await _repository.CommitAsync();
                    }

                });

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Erro ao executar o serviço!");
                return Task.CompletedTask;
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //// Add logging
            //serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
            //{
            //    builder
            //        .AddSerilog(dispose: true);
            //}));

            serviceCollection.AddLogging();

            //// Build configuration
            //configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            //    .AddJsonFile("appsettings.json", false)
            //    .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddPersistence(configuration);
            serviceCollection.AddRabbitMQ(configuration);
            serviceCollection.AddCore();
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);

            // Add app
            //serviceCollection.AddTransient<App>();
        }

    }
}


