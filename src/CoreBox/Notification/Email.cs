using CoreBox.Extensions;
using CoreBox.Notification.Validators;
using SendGrid.Helpers.Mail;

namespace CoreBox.Notification;

public class Email
{
    public EmailAddress From { get; set; }
    public List<EmailAddress> To { get; set; } = new List<EmailAddress>();
    public string Subject { get; set; }
    public string Content { get; set; }

    public Email(string from, string to, string subject, string content)
    {
        From = new EmailAddress(from?.Trim());
        Subject = subject;
        Content = content;
        
        if (to is not null && !to.Contains(','))
            To.Add(new EmailAddress(to.Trim()));
        else if (to is not null && to.Contains(','))
            foreach (var email in to.Split(','))
                To.Add(new EmailAddress(email.Trim()));
        
        this.ValidateAndThrow(new EmailValidator());
    }
}