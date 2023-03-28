using MediatR;
using TesteVericode.Domain.RabbitMQ;

namespace TesteVericode.Core.Handlers.Command
{
    public class DeleteTarefaCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string Command { get => "DeleteTarefaCommand"; }
        public DeleteTarefaCommand(int id)
        {
            this.Id = id;
        }
    }
    public class DeleteTarefaCommandHandler : IRequestHandler<DeleteTarefaCommand, int>
    {
        private IRabbitMQService _rabbitMQService;

        public DeleteTarefaCommandHandler(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        public async Task<int> Handle(DeleteTarefaCommand request, CancellationToken cancellationToken)
        {
            _rabbitMQService.SendMessage(request);

            return request.Id;
        }
    }
}
