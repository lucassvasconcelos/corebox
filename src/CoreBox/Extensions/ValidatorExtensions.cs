using FluentValidation;

namespace CoreBox.Extensions;

public static class ValidatorExtensions
{
    public static void ValidateAndThrow<T>(this T obj, AbstractValidator<T> validator)
        => validator.ValidateAndThrow<T>(obj);
}