using CoreBox.Validations;
using FluentValidation;

namespace CoreBox.Tests.Validations.Validator;

public class PessoaValidator : AbstractValidator<Pessoa>
{
    public PessoaValidator()
    {
        RuleFor(r => r.Documento).IsValidCpf().When(w => w.TipoPessoa == "Pessoa Física").WithMessage("O CPF é inválido!");
        RuleFor(r => r.Documento).IsValidCnpj().When(w => w.TipoPessoa == "Pessoa Jurídica").WithMessage("O CNPJ é inválido!");
    }
}