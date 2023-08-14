using CoreBox.Application;
using CoreBox.Repositories;
using MediatR;

namespace CoreBox.Tests.Application;

public class Handler : AbstractHandler
{
    public Handler(IMediator mediator, IUnitOfWork unitOfWork) : base(mediator, unitOfWork)
    {
    }
}