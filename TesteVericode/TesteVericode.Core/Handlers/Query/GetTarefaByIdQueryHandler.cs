using AutoMapper;
using MediatR;
using TesteVericode.Core.Exceptions;
using TesteVericode.Domain;
using TesteVericode.Domain.DTO;

namespace TesteVericode.Core.Handlers.Query
{
    public class GetTarefaByIdQuery : IRequest<TarefaDTO>
    {
        public int TarefaId { get; }

        public GetTarefaByIdQuery(int tarefaId)
        {
            TarefaId = tarefaId;
        }
    }

    public class GetTarefaByIdQueryHandler : IRequestHandler<GetTarefaByIdQuery, TarefaDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetTarefaByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TarefaDTO> Handle(GetTarefaByIdQuery request, CancellationToken cancellationToken)
        {
            var tarefa = await Task.FromResult(_repository.Tarefas.Get(request.TarefaId));

            if (tarefa == null)
            {
                throw new EntityNotFoundException($"No tarefa found for Id {request.TarefaId}");
            }

            return _mapper.Map<TarefaDTO>(tarefa);
        }
    }
}
