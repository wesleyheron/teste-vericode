using TesteVericode.Domain.Entities;
using TesteVericode.Domain.Repositories;
using TesteVericode.Migrations;

namespace TesteVericode.Infrastructure.Repositories
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(VericodeDbContext context) : base(context) { }
    }
}
