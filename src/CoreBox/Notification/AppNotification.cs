using FluentValidation.Results;
using MediatR;

namespace CoreBox.Notification;

public class AppNotification : INotification
{
    public int? HttpStatusCode { get; private set; }
    public List<string> Errors { get; private set; } = new();

    public AppNotification(int httpStatusCode, string error)
    {
        HttpStatusCode = httpStatusCode;
        Errors = new List<string> { error };
        Validate();
    }

    public AppNotification(int httpStatusCode, List<string> errors = null)
    {
        HttpStatusCode = httpStatusCode;
        Errors = errors;
        Validate();
    }

    public AppNotification(int httpStatusCode, List<ValidationFailure> errors = null)
    {
        HttpStatusCode = httpStatusCode;
        Errors = errors.Select(e => e.ErrorMessage).ToList();
        Validate();
    }

    private void Validate()
    {
        if (HttpStatusCode < 400)
            throw new Exception("Não é possível criar uma notificação de sucesso!");

        if (Errors.Any(e => e == null))
            throw new Exception("Não é permitido criar uma notificação sem mensagem!");
    }
}