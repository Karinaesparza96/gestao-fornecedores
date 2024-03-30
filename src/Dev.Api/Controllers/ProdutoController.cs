using AutoMapper;
using Dev.Api.ViewModels;
using Dev.Business.Interfaces;
using Dev.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dev.Api.Controllers
{
    [Route("api/produtos")]
    public class ProdutoController : MainController
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoController(INotificador notificador,
                                 IProdutoService produtoService,
                                 IProdutoRepository produtoRepository,
                                 IMapper mapper)
                                : base(notificador)
        {
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos(int pagIndex, int pagSize, string query = null)
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos(pagIndex, pagSize, query));
        }

        [HttpGet("{id:guid}")]

        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            var produto = await ObterProdutoFornecedor(id);

            if (produto == null)
            {
                return NotFound();
            }

            return CustomResponse(HttpStatusCode.OK, produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var produto = _mapper.Map<Produto>(produtoViewModel);

            await _produtoService.Adicionar(produto);

            return CustomResponse(HttpStatusCode.Created, produtoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            if(id != produtoViewModel.Id)
            {
                NotificarErro("O Id informado não é o mesmo que foi passado na query.");
                return CustomResponse();
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

           await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));
         
           return CustomResponse(HttpStatusCode.NoContent); 
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
             await _produtoService.Remover(id);

            return CustomResponse();    
        }

        private async Task<ProdutoViewModel> ObterProdutoFornecedor(Guid id)
        {
           return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
        }
    }
}
