using FluentValidation;

namespace CoreBox.Validations;

public static partial class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TEnum> IsValidEnum<T, TEnum>(this IRuleBuilder<T, TEnum> ruleBuilder)
        => ruleBuilder.SetValidator(new EnumValidator<T, TEnum>());
}