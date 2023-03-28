using Microsoft.EntityFrameworkCore;
using TesteVericode.Domain.Entities;
using TesteVericode.Migrations.Mappings;

namespace TesteVericode.Migrations
{
    public class VericodeDbContext : DbContext
    {
        public VericodeDbContext()
        {

        }
        public VericodeDbContext(DbContextOptions<VericodeDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
