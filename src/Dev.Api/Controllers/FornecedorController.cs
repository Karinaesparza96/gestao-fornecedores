﻿using AutoMapper;
using Dev.Api.DTOs;
using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Business.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dev.Api.Controllers
{
    [Authorize]
    [Route("api/fornecedores")]
    public class FornecedorController : MainController
    {   
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        public FornecedorController(INotificador notificador, 
                                    IFornecedorService fornecedorService, 
                                    IFornecedorRepository fornecedorRepository,
                                    IMapper mapper,
                                    IAppIdentityUser user) : base(notificador, user)
        {
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            var fornecedores = await _fornecedorRepository.ObterFornecedoresProdutosEndereco();
            return _mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedores);
         
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]

        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            await _fornecedorService.Adicionar(fornecedor);

            fornecedorViewModel = _mapper.Map<FornecedorViewModel>(fornecedor);

           return CustomResponse(HttpStatusCode.Created, fornecedorViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        {   
            if(id != fornecedorViewModel.Id)
            {
                NotificarErro("O Id informado não é o mesmo que foi passado na query.");
                CustomResponse();
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorViewModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
           await _fornecedorService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }


        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
           return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
