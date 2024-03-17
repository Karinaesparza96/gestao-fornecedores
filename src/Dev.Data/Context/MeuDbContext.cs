using Dev.Business.Models;
using Dev.Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            //Debugger.Launch();
            modelBuilder.SetStringColumnType();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                // Impede o comportamento de deletar em cascata
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entity.ClrType))
                {
                    var parameter = Expression.Parameter(entity.ClrType, "e");
                    var property = Expression.Property(parameter, "Excluido");
                    var condition = Expression.Not(property);
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(lambda);
                }
            }

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
