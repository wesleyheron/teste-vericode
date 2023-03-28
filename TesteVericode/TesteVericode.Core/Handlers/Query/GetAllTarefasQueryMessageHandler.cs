using AutoMapper;
using MediatR;
using TesteVericode.Domain;
using TesteVericode.Domain.DTO;

namespace TesteVericode.Core.Handlers.Query
{

    public class GetAllTarefasQuery : IRequest<IEnumerable<TarefaDTO>>
    {
        public string Command { get => "GetAllTarefasQuery"; }
    }

    public class GetAllTarefasQueryMessageHandler : IRequestHandler<GetAllTarefasQuery, IEnumerable<TarefaDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllTarefasQueryMessageHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TarefaDTO>> Handle(GetAllTarefasQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Tarefas.GetAll().ToList());
            return _mapper.Map<IEnumerable<TarefaDTO>>(entities);
        }
    }
}
