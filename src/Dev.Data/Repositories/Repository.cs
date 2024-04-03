using Dev.Business.Models.Base;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dev.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {   
        private readonly MeuDbContext _context;
        protected DbSet<TEntity> DbSet { get; set; }
        protected Repository(MeuDbContext context)
        { 
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.AsNoTracking().Where(expression).ToListAsync();
        }
        public virtual void Adicionar(TEntity entity)
        {
             DbSet.Add(entity);
        }

        public virtual void Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void  Remover(Guid id)
        {
           DbSet.Remove(new TEntity { Id = id });
        }

        public async Task<int> SaveChanges()
        {
          return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
