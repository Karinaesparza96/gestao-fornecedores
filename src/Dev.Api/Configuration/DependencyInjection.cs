using Dev.Api.Extensions.IdentityUser;
using Dev.Business.Interfaces;
using Dev.Business.Models.Base;
using Dev.Business.Noficacoes;
using Dev.Business.Services;
using Dev.Data.Context;
using Dev.Data.Repositories;
using Dev.Data.UoW;

namespace Dev.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {   
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();

            // Data
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            // Business
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<INotificador, Notificador>();


            return services;
        }
    }
}
