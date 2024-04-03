using Dev.Business.Models;
using Dev.Business.Models.Base;

namespace Dev.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {   
        Task<Produto> ObterProdutoFornecedor(Guid id);
        Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<PaginateList<Produto>> ObterTodosPaginado(int pagIndex, int pagSize, string query);
    }
}
