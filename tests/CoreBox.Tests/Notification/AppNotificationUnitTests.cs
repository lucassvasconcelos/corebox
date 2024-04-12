using System;
using System.Collections.Generic;
using System.Linq;
using CoreBox.Notification;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace CoreBox.Tests.Notification;

public class AppNotificationUnitTests
{
    [Fact]
    public void Nao_Deve_Criar_Uma_Notificacao_De_Sucesso()
    {
        Action act = () => new AppNotification(200, "Sucesso!");
        act.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Nao_Deve_Criar_Uma_Notificacao_De_Sem_Conteudo()
    {
        Action act = () => new AppNotification(400, (string)null);
        act.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Nao_Deve_Criar_Uma_Lista_De_Notificacoes_Sem_Erros()
    {
        Action act = () => new AppNotification(400, new List<string>());
        act.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Nao_Deve_Criar_Uma_Lista_De_Notificacoes_Sem_Erros_De_Validacao()
    {
        Action act = () => new AppNotification(400, new List<ValidationFailure>());
        act.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Deve_Criar_Uma_Notificacao_De_Falha_Com_Um_Erro()
    {
        int httpStatus = 404;
        string erro = "Item não encontrado";

        var notification = new AppNotification(httpStatus, erro);
        notification.HttpStatusCode.Should().Be(httpStatus);
        notification.Errors.Should().HaveCount(1);
        notification.Errors.First().Should().Be(erro);
    }

    [Fact]
    public void Deve_Criar_Uma_Notificacao_De_Falha_Com_Varios_Erros_De_Validacao()
    {
        int httpStatus = 400;
        string erro1 = "Item 1X Inválido";
        string erro2 = "Item 2X Inválido";
        List<string> erros = new() { erro1, erro2 };

        var notification = new AppNotification(httpStatus, erros);
        notification.HttpStatusCode.Should().Be(httpStatus);
        notification.Errors.Should().HaveCount(2);
        notification.Errors.Any(e => e == erro1).Should().Be(true);
        notification.Errors.Any(e => e == erro2).Should().Be(true);
    }

    [Fact]
    public void Deve_Criar_Uma_Notificacao_De_Falha_Com_ValidationResult()
    {
        int httpStatus = 400;
        var (product, validationResult) = Product.Create(0, null);

        var notification = new AppNotification(httpStatus, validationResult.Errors);
        notification.HttpStatusCode.Should().Be(httpStatus);
        notification.Errors.Should().HaveCount(2);
        notification.Errors.Any(e => e == "Identificador deve ser maior que zero").Should().Be(true);
        notification.Errors.Any(e => e == "Descrição requerida!").Should().Be(true);
    }
}