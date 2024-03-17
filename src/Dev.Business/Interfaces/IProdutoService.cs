using Dev.Business.Models;

namespace Dev.Business.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adionar(Produto produto);

        Task Atulizar(Produto produto);

        Task Remover(Guid id);
    }
}
