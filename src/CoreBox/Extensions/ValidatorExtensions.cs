using FluentValidation;

namespace CoreBox.Extensions;

public static class ValidatorExtensions
{
    public static void ValidateAndThrow<T>(this T obj, AbstractValidator<T> validator)
    {
        var result = validator.Validate(obj);

        if (!result.IsValid)
        {
            var message = string.Join("\r\n", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(message, result.Errors);
        }
    }

    public static void ValidateNullAndThrow<T>(this T obj, string message)
    {
        if (obj is null)
            throw new ValidationException(message);
    }
}