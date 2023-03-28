using FluentValidation;
using MediatR;
using TesteVericode.Core.Exceptions;
using TesteVericode.Domain.DTO;
using TesteVericode.Domain.RabbitMQ;

namespace TesteVericode.Core.Handlers.Command
{
    public class UpdateTarefaCommand : IRequest<int>
    {
        public int Id { get; set; }
        public UpdateTarefaDTO Model { get; }
        public string Command { get => "UpdateTarefaCommand"; }
        public UpdateTarefaCommand(UpdateTarefaDTO model, int id)
        {
            this.Model = model;
            this.Id = id;
        }
    }

    public class UpdateTarefaCommandHandler : IRequestHandler<UpdateTarefaCommand, int>
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IValidator<UpdateTarefaDTO> _validator;

        public UpdateTarefaCommandHandler(IRabbitMQService rabbitMQService, IValidator<UpdateTarefaDTO> validator)
        {
            _rabbitMQService = rabbitMQService;
            _validator = validator;
        }

        public async Task<int> Handle(UpdateTarefaCommand request, CancellationToken cancellationToken)
        {
            UpdateTarefaDTO model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            _rabbitMQService.SendMessage(request);

            return request.Id;

        }
    }
}
