using Dev.Business.Models.Validations.Documentos;
using FluentValidation;

namespace Dev.Business.Models.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(c => c.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} e for fornecido {PropertyValue}.");

                RuleFor(c => CpfValidacao.Validar(c.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");

            });

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(c => c.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} e for fornecido {PropertyValue}.");
            });
        }
    }
}
