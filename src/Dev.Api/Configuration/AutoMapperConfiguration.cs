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
            CreateMap<FornecedorViewModel, Fornecedor>()
                .ForMember( dest => dest.Id, src => src.Ignore());

            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<EnderecoViewModel, Endereco>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<ProdutoViewModel, Produto>()
                .ForMember(dest => dest.Id, src => src.Ignore());
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome));

        }
    }
}
