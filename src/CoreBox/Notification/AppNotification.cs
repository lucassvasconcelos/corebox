using FluentValidation.Results;
using MediatR;

namespace CoreBox.Notification;

public class AppNotification : INotification
{
    public int HttpStatusCode { get; private set; }
    public List<string> Errors { get; private set; }

    public AppNotification(int httpStatusCode, string error)
    {
        ValidateStatusCode(httpStatusCode);
        HttpStatusCode = httpStatusCode;

        if (string.IsNullOrEmpty(error))
            throw new ArgumentException("Não é possível criar uma notificação sem mensagem!");

        Errors = [error];
    }

    public AppNotification(int httpStatusCode, List<string> errors)
    {
        ValidateStatusCode(httpStatusCode);
        HttpStatusCode = httpStatusCode;

        if (errors.Count == 0)
            throw new ArgumentException("Não é possível criar uma notificação sem mensagens!");
        
        Errors = errors;
    }

    public AppNotification(int httpStatusCode, List<ValidationFailure> errors)
    {
        ValidateStatusCode(httpStatusCode);
        HttpStatusCode = httpStatusCode;

        if (errors.Count == 0)
            throw new ArgumentException("Não é possível criar uma notificação sem mensagens!");

        Errors = errors.Select(e => e.ErrorMessage).ToList();
    }

    private static void ValidateStatusCode(int httpStatusCode)
    {
        if (httpStatusCode < 400)
            throw new ArgumentException("Não é possível criar uma notificação de sucesso!");
    }
}