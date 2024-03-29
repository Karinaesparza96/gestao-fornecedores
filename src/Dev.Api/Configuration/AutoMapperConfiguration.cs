using AutoMapper;
using Dev.Api.DTOs;
using Dev.Api.ViewModels;
using Dev.Business.Models;

namespace Dev.Api.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Fornecedor, FornecedorViewModel>();
            CreateMap<FornecedorViewModel, Fornecedor>();

            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<EnderecoViewModel, Endereco>();


            CreateMap<ProdutoViewModel, Produto>();
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome));

        }
    }
}
