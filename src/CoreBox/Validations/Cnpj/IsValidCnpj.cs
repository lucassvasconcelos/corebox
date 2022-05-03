using FluentValidation;

namespace CoreBox.Validations;

public static partial class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> IsValidCnpj<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder.SetValidator(new CnpjValidator<T, string>());
}