using TesteVericode.Domain;
using TesteVericode.Domain.Repositories;
using TesteVericode.Infrastructure.Repositories;
using TesteVericode.Migrations;

namespace TesteVericode.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VericodeDbContext _context;

        public UnitOfWork(VericodeDbContext context)
        {
            _context = context;
        }
        public ITarefaRepository Tarefas => new TarefaRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
