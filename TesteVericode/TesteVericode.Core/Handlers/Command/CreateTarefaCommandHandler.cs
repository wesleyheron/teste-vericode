using FluentValidation;
using MediatR;
using TesteVericode.Core.Exceptions;
using TesteVericode.Domain.DTO;
using TesteVericode.Domain.Entities;
using TesteVericode.Domain.RabbitMQ;

namespace TesteVericode.Core.Handlers.Command
{
    public class CreateTarefaCommand : IRequest<int>
    {
        public CreateTarefaDTO Model { get; }

        public string Command { get => "CreateTarefaCommand"; }

        public CreateTarefaCommand(CreateTarefaDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateTarefaCommandHandler : IRequestHandler<CreateTarefaCommand, int>
    {
        private readonly IValidator<CreateTarefaDTO> _validator;
        private readonly IRabbitMQService _rabbitMQService;

        public CreateTarefaCommandHandler(
            IValidator<CreateTarefaDTO> validator, 
            IRabbitMQService rabbitMQService)
        {
            _validator = validator;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<int> Handle(CreateTarefaCommand request, CancellationToken cancellationToken)
        {
            CreateTarefaDTO model = request.Model;

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

            return request.Model.Id;
        }
    }   

}
