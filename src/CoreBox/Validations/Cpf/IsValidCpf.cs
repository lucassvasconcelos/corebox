using FluentValidation;

namespace CoreBox.Validations;

public static partial class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> IsValidCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder.SetValidator(new CpfValidator<T, string>());
}