using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using TesteVericode.Domain;
using TesteVericode.Domain.RabbitMQ;
using TesteVericode.Infrastructure.RabbitMQ;
using TesteVericode.Migrations;

namespace TesteVericode.Infrastructure
{
    public static class ServiceExtensions
    {
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlite<VericodeDbContext>(configuration.GetConnectionString("DefaultConnection"), (options) =>
            {
                options.MigrationsAssembly("TesteVericode.Migrations");
            });
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork(); 
        }

        public static IServiceCollection AddHostedService(this IServiceCollection services)
        {
            return services.AddHostedService<RabbitMQConsumer>();
        }

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();
            var factory = new ConnectionFactory
            {
                HostName = rabbitMQConfig.Hostname,
                UserName = rabbitMQConfig.Username,
                Password = rabbitMQConfig.Password
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: rabbitMQConfig.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            services.AddSingleton(connection);
            services.AddSingleton(channel);

            services.AddSingleton<IRabbitMQService, RabbitMQService>();           

            return services;
        }
    }
}
