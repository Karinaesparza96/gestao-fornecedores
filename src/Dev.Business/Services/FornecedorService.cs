using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Business.Models.Base;
using Dev.Business.Models.Validations;

namespace Dev.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        public FornecedorService(INotificador notificador,
                                IFornecedorRepository fornecedorRepository,
                                IUnitOfWork unitOfWork) : base(notificador, unitOfWork)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            if (fornecedor.Endereco != null &&
                !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

            var fornecedores = await _fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento);

            if (fornecedores.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return;
            }

            _fornecedorRepository.Adicionar(fornecedor);
            await Commit();
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            var fornecedores = await _fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);

            if (fornecedores.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return;
            }

            _fornecedorRepository.Atualizar(fornecedor);
            await Commit();
        }

        public async Task Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null)
            {
                Notificar("Fornecedor não existe.");
                return;
            }

            if (fornecedor.Produtos != null && fornecedor.Produtos.Any())
            {
                Notificar("Fornecedor possui produtos cadastrados.");
                return;
            }

            var endereco = await _fornecedorRepository.ObterEnderecoPorFornecedor(id);

            if (endereco != null)
            {
                _fornecedorRepository.RemoverEnderecoFornecedor(endereco);
            }

            _fornecedorRepository.Remover(id);

            await Commit();
        }
        public void Dispose()
        {
            _fornecedorRepository.Dispose();
        }

    }
}
