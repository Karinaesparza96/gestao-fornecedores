using Dev.Business.Models;
namespace Dev.Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);

        Task<Fornecedor> ObterFornecedorEndereco(Guid id);

        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);

        Task RemoverEnderecoFornecedor(Endereco endereco);
    }
}
