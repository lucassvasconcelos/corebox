using System.Net;
using System.Text.Json;
using CoreBox.Exceptions;
using CoreBox.Notification.Abstractions;
using CoreBox.Notification.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CoreBox.Notification;

public class SendGridService : ISmtpService
{
    private readonly ISendGridClient _sendGridClient;

    public SendGridService(ISendGridClient sendGridClient)
        => _sendGridClient = sendGridClient;

    public async Task SendEmail(Email email)
    {
        var response = await _sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmailToMultipleRecipients(email.From, email.To, email.Subject, email.Content, email.Content));

        if (response.StatusCode is not HttpStatusCode.Accepted)
        {
            var errorResponse = JsonSerializer.Deserialize<SendGridErrorResponse>(await response.Body.ReadAsStringAsync());
            throw new SendGridException($"Ocorreu um erro({response.StatusCode}) ao enviar o e-mail: {errorResponse.errors[0].message}");
        }
    }
}