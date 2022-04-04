using FluentValidation;
using SendGrid.Helpers.Mail;

namespace CoreBox.Notification.Validators;

public class SendGridEmailValidator : AbstractValidator<EmailAddress>
{
    public SendGridEmailValidator(string text)
    {
        RuleFor(r => r.Email).NotEmpty().WithMessage($"Informe quem será o {text} da mensagem");
        RuleFor(r => r.Email).EmailAddress().WithMessage($"O e-mail do {text} é inválido");
    }
}