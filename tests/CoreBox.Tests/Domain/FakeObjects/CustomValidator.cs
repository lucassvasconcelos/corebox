using FluentValidation;

namespace CoreBox.Tests
{
    public class CustomValidator : AbstractValidator<Validation>
    {
        public CustomValidator()
            => RuleFor(r => r.CampoObrigatorio).NotEmpty().WithMessage("Campo obrigatório!");
    }
}