namespace CoreBox.Notification.Abstractions;

public interface ISmtpService
{
    Task SendEmail(Email email);
}