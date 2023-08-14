using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreBox.Notification;
using CoreBox.Repositories;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using Moq;
using Xunit;

namespace CoreBox.Tests.Application;

public class AbstractHandlerUnitTests
{
    private readonly Handler _handler;
    private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
    private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();

    public AbstractHandlerUnitTests()
    {
        _handler = new Handler(_mockMediator.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Deve_Enviar_Uma_Notificacao_Caso_A_Condicao_Atenda_Todos_Os_Requisitos()
    {
        _mockMediator.Setup(s => s.Publish(It.IsAny<AppNotification>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var response = await _handler.NotificationHasBeenPublishedAsync(true, 400, "Test");

        response.Should().BeTrue();
        _mockMediator.Verify(v => v.Publish(It.IsAny<AppNotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Enviar_Uma_Notificacao_Pois_A_Condicao_Nao_Atende_Todos_Os_Requisitos()
    {
        _mockMediator.Setup(s => s.Publish(It.IsAny<AppNotification>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var response = await _handler.NotificationHasBeenPublishedAsync(false, 400, "Test");

        response.Should().BeFalse();
        _mockMediator.Verify(v => v.Publish(It.IsAny<AppNotification>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Deve_Enviar_Uma_Lista_De_Notificacoes_Caso_A_Condicao_Atenda_Todos_Os_Requisitos()
    {
        _mockMediator.Setup(s => s.Publish(It.IsAny<AppNotification>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var response = await _handler.NotificationHasBeenPublishedAsync(true, 400, new List<ValidationFailure> { new ValidationFailure("Field1", "Message"), new ValidationFailure("Field2", "Message") });

        response.Should().BeTrue();
        _mockMediator.Verify(v => v.Publish(It.IsAny<AppNotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Enviar_Uma_Lista_De_Notificacoes_Pois_A_Condicao_Nao_Atende_Todos_Os_Requisitos()
    {
        _mockMediator.Setup(s => s.Publish(It.IsAny<AppNotification>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var response = await _handler.NotificationHasBeenPublishedAsync(false, 400, new List<ValidationFailure> { });

        response.Should().BeFalse();
        _mockMediator.Verify(v => v.Publish(It.IsAny<AppNotification>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}