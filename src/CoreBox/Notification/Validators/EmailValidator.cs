using FluentValidation;

namespace CoreBox.Notification.Validators;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(r => r.From).SetValidator(new SendGridEmailValidator("rementente"));
        RuleFor(r => r.To).NotEmpty().WithMessage("A lista de destinatários não pode ser nula");
        RuleForEach(r => r.To).SetValidator(new SendGridEmailValidator("destinatário")).When(w => w.To is not null);
        RuleFor(r => r.Subject).NotEmpty().WithMessage("Informe o assunto da mensagem");
        RuleFor(r => r.Content).NotEmpty().WithMessage("Informe o conteúdo da mensagem");
    }
}