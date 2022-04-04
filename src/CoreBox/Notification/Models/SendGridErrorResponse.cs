namespace CoreBox.Notification.Models;

public class SendGridErrorResponse
{
    public List<SendGridErrorResponseItem> errors { get; set; } = new List<SendGridErrorResponseItem>();
}