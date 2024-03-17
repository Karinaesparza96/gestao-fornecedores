using Dev.Business.Models;

namespace Dev.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {   
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
