using Dev.Business.Interfaces;
using Dev.Business.Noficacoes;
using Dev.Business.Services;
using Dev.Data.Context;
using Dev.Data.Repositories;

namespace Dev.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {   
            // Data
            services.AddScoped<MeuDbContext>();
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
