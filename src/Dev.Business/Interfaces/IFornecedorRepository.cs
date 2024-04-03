using Dev.Business.Models;
using Dev.Business.Models.Base;
namespace Dev.Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {   
        Task<IEnumerable<Fornecedor>> ObterFornecedoresProdutosEndereco();
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);

        Task<Fornecedor> ObterFornecedorEndereco(Guid id);

        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);

        void RemoverEnderecoFornecedor(Endereco endereco);
    }
}
