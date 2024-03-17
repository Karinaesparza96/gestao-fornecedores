using Dev.Business.Models;
using System.Linq.Expressions;

namespace Dev.Business.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity, new()
    {
        Task Adicionar(TEntity entity);

        Task<TEntity> ObterPorId(Guid id);

        Task<List<TEntity>> ObterTodos();

        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> expression);

        Task Excluir(Guid id);

        Task Atualizar(TEntity entity);

        Task<int> SaveChanges();
    }
}
