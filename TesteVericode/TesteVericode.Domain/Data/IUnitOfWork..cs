using TesteVericode.Domain.Repositories;

namespace TesteVericode.Domain
{
    public interface IUnitOfWork
    {
        ITarefaRepository Tarefas { get; }

        Task CommitAsync();
    }
}
