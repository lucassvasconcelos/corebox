using System;
using CoreBox.Notification;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests.Notification;

public class EmailUnitTests
{
    [Theory]
    [InlineData("email@email.com", "email@email.com", "subject", "content")]
    public void Deve_Construir_O_Objeto_Email(string from, string to, string subject, string content)
    {
        Email email = new(from, to, subject, content);

        email.From.Email.Should().Be(from);
        email.To[0].Email.Should().Be(to);
        email.Subject.Should().Be(subject);
        email.Content.Should().Be(content);
    }

    [Theory]
    [InlineData(null, "email@email.com", "subject", "content", "Informe quem será o remetente da mensagem")]
    [InlineData("", "email@email.com", "subject", "content", "Informe quem será o remetente da mensagem")]
    [InlineData("emailIncorreto", "email@email.com", "subject", "content", "O e-mail do remetente é inválido")]
    [InlineData("email@email.com", null, "subject", "content", "A lista de destinatários não pode ser nula")]
    [InlineData("email@email.com", "", "subject", "content", "Informe quem será o destinatário da mensagem")]
    [InlineData("email@email.com", "emailIncorreto", "subject", "content", "O e-mail do destinatário é inválido")]
    [InlineData("email@email.com", "email@email.com", null, "content", "Informe o assunto da mensagem")]
    [InlineData("email@email.com", "email@email.com", "", "content", "Informe o assunto da mensagem")]
    [InlineData("email@email.com", "email@email.com", "subject", null, "Informe o conteúdo da mensagem")]
    [InlineData("email@email.com", "email@email.com", "subject", "", "Informe o conteúdo da mensagem")]
    [InlineData("email@email.com", "email@email.com,emailIncorreto", "subject", "content", "O e-mail do destinatário é inválido")]
    public void Deve_Emitir_Um_Erro_De_Validacao_Por_Inconsistencia_Do_Email(string from, string to, string subject, string content, string errorMessage)
    {
        Action act = () => new Email(from, to, subject, content);
        act.Should().ThrowExactly<ValidationException>(errorMessage);
    }
}