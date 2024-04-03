using Dev.Business.Models;
using Dev.Business.Models.Base;

namespace Dev.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {   
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
