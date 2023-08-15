using CoreBox.Notification;
using CoreBox.Repositories;
using FluentValidation.Results;
using MediatR;

namespace CoreBox.Application;

public abstract class AbstractHandler
{
    protected readonly IMediator _mediator;
    protected readonly IUnitOfWork _unitOfWork;

    public AbstractHandler(
        IMediator mediator,
        IUnitOfWork unitOfWork
    )
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> NotificationHasBeenPublishedAsync(bool condition, int httpStatusCode, string messageToNotify)
    {
        if (!condition) return false;

        await _mediator.Publish(new AppNotification(httpStatusCode, messageToNotify), default);
        return true;
    }

    public async Task<bool> NotificationHasBeenPublishedAsync(bool condition, int httpStatusCode, List<string> errors)
    {
        if (!condition) return false;

        await _mediator.Publish(new AppNotification(httpStatusCode, errors), default);
        return true;
    }

    public async Task<bool> NotificationHasBeenPublishedAsync(bool condition, int httpStatusCode, List<ValidationFailure> errors)
    {
        if (!condition) return false;

        await _mediator.Publish(new AppNotification(httpStatusCode, errors), default);
        return true;
    }
}