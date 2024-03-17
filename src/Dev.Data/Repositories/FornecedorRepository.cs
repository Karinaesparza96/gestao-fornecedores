using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dev.Data.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {   
        private readonly IEnderecoRepository _enderecoRepository;
        public FornecedorRepository(MeuDbContext context, 
            IEnderecoRepository enderecoRepository) : base(context)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await _enderecoRepository.ObterEnderecoPorFornecedor(fornecedorId);
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
           return await DbSet.AsNoTracking()
                .Include(f => f.Endereco)
                .FirstAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(f => f.Produtos)
                .Include(f => f.Endereco)
                .FirstAsync(f => f.Id == id);
        }

        public async Task RemoverEnderecoFornecedor(Endereco endereco)
        {   
           await _enderecoRepository.Excluir(endereco.Id);
        }
    }
}
