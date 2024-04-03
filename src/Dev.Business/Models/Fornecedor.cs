using Dev.Business.Models.Base;

namespace Dev.Business.Models
{
    public class Fornecedor : Entity
    {
        public string? Nome { get; set; }

        public string? Documento { get; set; }

        public TipoFornecedor TipoFornecedor { get; set; }

        public IEnumerable<Produto>? Produtos { get; set; }

        public Endereco? Endereco { get; set; }
    }
}
