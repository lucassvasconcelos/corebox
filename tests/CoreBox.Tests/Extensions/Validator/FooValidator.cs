using FluentValidation;

namespace CoreBox.Tests.Extensions.Validator
{
    public class FooValidator : AbstractValidator<Foo>
    {
        public FooValidator()
        {
            RuleFor(rule => rule.Descricao)
                .NotEmpty()
                .WithMessage("Descricao requerida");
        }
    }
}