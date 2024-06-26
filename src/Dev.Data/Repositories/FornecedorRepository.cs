﻿using Dev.Business.Interfaces;
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

        public async Task<IEnumerable<Fornecedor>> ObterFornecedoresProdutosEndereco()
        {
            return await DbSet.AsNoTracking()
                             .Include(f => f.Endereco)
                             .Include(f => f.Produtos)
                             .OrderBy(f => f.Nome)
                             .ToListAsync();
                    
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await DbSet.AsNoTracking()
                            .Include(f => f.Endereco)
                            .Include(f => f.Produtos)
                            .OrderBy(f => f.Nome)
                            .FirstAsync(f => f.Id == id);
        }

        public void RemoverEnderecoFornecedor(Endereco endereco)
        {   
            _enderecoRepository.Remover(endereco.Id);
        }
    }
}
