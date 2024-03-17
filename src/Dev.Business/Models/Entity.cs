
namespace Dev.Business.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public bool Excluido {  get; set; }

        public DateTime DataCadastro { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
