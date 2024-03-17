using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dev.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context)
        {
        }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(p => p.Fornecedor)
                .FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await DbSet.AsNoTracking()
                .Include(p => p.Fornecedor)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);   
        }
    }
}
