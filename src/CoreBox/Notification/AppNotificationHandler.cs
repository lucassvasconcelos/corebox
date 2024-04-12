using MediatR;

namespace CoreBox.Notification;

public class AppNotificationHandler : INotificationHandler<AppNotification>
{
    private AppNotification? _appNotification;

    public async Task Handle(AppNotification notification, CancellationToken cancellationToken)
    {
        _appNotification = notification;
        await Task.CompletedTask;
    }

    public virtual AppNotification? GetNotificationContent()
        => _appNotification;

    public virtual bool HasErrors()
        => _appNotification is not null && _appNotification.Errors is not null && _appNotification.Errors.Count != 0;

    public void Dispose()
        => _appNotification = null;
}