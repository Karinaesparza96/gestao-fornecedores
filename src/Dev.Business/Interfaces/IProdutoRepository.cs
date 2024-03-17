using Dev.Business.Models;

namespace Dev.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {   
        Task<Produto> ObterProdutoFornecedor(Guid id);
        Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
    }
}
