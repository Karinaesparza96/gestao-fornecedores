using Dev.Business.Models.Base;

namespace Dev.Business.Models
{
    public class Produto : Entity
    {   
        public Guid FornecedorId { get; set; }
        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataCadastro { get; set; }

        /* EF Relation */

        public Fornecedor? Fornecedor { get; set; }
    }
}
