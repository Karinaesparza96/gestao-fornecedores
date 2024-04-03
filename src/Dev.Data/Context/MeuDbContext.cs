using Dev.Business.Models.Base;
using Dev.Data.Context.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dev.Data.Context
{
    public class MeuDbContext : IdentityDbContext
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
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
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

        
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
