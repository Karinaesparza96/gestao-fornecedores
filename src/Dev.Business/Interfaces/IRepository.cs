using Dev.Business.Models;
using System.Linq.Expressions;

namespace Dev.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);

        Task<TEntity> ObterPorId(Guid id);

        Task<List<TEntity>> ObterTodos();

        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> expression);

        Task Remover(Guid id);

        Task Atualizar(TEntity entity);

        Task<int> SaveChanges();
    }
}
