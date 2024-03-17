using Dev.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Dev.Data.Mapping
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(f => f.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");


            // 1 : 1 => Fornecedor : Endereço

            builder.HasOne(f => f.Endereco)
                  .WithOne(e => e.Fornecedor);

            // 1 : N => Fornecedor : Produtos

            builder
                .HasMany(f => f.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey("FornecedorId");

            builder.ToTable("Fornecedores");
        }
    }
}
