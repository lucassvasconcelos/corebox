using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBox.Notification;
using FluentAssertions;
using MediatR;
using Xunit;

namespace CoreBox.Tests.Notification;

public class AppNotificationHandlerUnitTests
{
    private readonly AppNotificationHandler _handler;

    public AppNotificationHandlerUnitTests()
        => _handler = new AppNotificationHandler();

    [Fact]
    public async Task Deve_Publicar_Um_Evento_De_Notificacao()
    {
        await _handler.Handle(new AppNotification(400, "Erro"), default);
    }

    [Fact]
    public async Task Deve_Publicar_Um_Evento_De_Notificacao_E_Depois_Obter_Seu_Conteudo()
    {
        var error = new AppNotification(400, "Erro");
        await _handler.Handle(error, default);

        var hasErrors = _handler.HasErrors();
        hasErrors.Should().Be(true);
        var content = _handler.GetNotificationContent();
        content.Should().Be(error);

        _handler.Dispose();
        _handler.GetNotificationContent().Should().BeNull();

        hasErrors = _handler.HasErrors();
        hasErrors.Should().Be(false);
    }
}