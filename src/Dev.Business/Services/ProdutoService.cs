using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Business.Models.Validations;

namespace Dev.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {   
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(INotificador notificador, IProdutoRepository produtoRepository) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            var produtoExistente = _produtoRepository.ObterPorId(produto.Id);

            if(produtoExistente != null)
            {
                Notificar("Já existe um produto com o Id informado.");
                return;
            }

            await _produtoRepository.Adicionar(produto);

        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }
    }
}
