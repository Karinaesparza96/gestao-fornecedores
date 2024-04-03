namespace Dev.Business.Models.Base
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime DataCadastro { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
