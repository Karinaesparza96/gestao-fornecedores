using Dev.Business.Models;
using Dev.Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Dev.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> db) : base(db)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.SetStringColumnType();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                // Impede o comportamento de deletar em cascata
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.AplicarFiltroSoftDelete<Entity>();

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<Entity>())
            { 
                if(entry.State == EntityState.Added) 
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if(entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }

                if(entry.State == EntityState.Deleted)
                {
                    entry.Property("Excluido").CurrentValue = true;
                    entry.State = EntityState.Modified;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
