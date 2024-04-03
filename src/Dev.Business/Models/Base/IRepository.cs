using System.Linq.Expressions;

namespace Dev.Business.Models.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();

        void Adicionar(TEntity entity);

        void Remover(Guid id);

        void Atualizar(TEntity entity);

        Task<int> SaveChanges();
    }
}
