using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using CoreBox.Exceptions;
using CoreBox.Extensions;
using CoreBox.Notification;
using CoreBox.Notification.Models;
using CoreBox.Tests.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;
using Xunit;

namespace CoreBox.Tests.Notification;

public class SendGridServiceUnitTests
{
    [Theory, AutoMoqData]
    public async Task Deve_Simular_O_Envio_De_Email_Via_SendGrid_Service(Mock<ISendGridClient> mockSendGridClient)
    {
        var service = new SendGridService(mockSendGridClient.Object);
        
        var email = new Email("corebox@test.com", "lucas@gmail.com", "CoreBox - SendGrid E-mail Test", "Hello World");

        Response response = new(HttpStatusCode.Accepted, null, null);

        mockSendGridClient.Setup(s => s.SendEmailAsync(It.IsAny<SendGridMessage>(), default)).ReturnsAsync(response);

        await service.SendEmail(email);

        mockSendGridClient.Verify(v => v.SendEmailAsync(It.IsAny<SendGridMessage>(), default), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task Deve_Simular_Um_Erro_No_Envio_De_Email_Via_SendGrid_Service(Mock<ISendGridClient> mockSendGridClient, SendGridErrorResponseItem errorItem)
    {
        var service = new SendGridService(mockSendGridClient.Object);
        
        var email = new Email("corebox@test.com", "lucas@gmail.com", "CoreBox - SendGrid E-mail Test", "Hello World");

        var errorResponse = new SendGridErrorResponse();
        errorResponse.errors.Add(errorItem);

        var httpContent = new StringContent(JsonSerializer.Serialize(errorResponse));

        Response response = new(HttpStatusCode.Unauthorized, httpContent, null);

        mockSendGridClient.Setup(s => s.SendEmailAsync(It.IsAny<SendGridMessage>(), default)).ReturnsAsync(response);
        
        Func<Task> act = async () => await service.SendEmail(email);
        await act.Should().ThrowExactlyAsync<SendGridException>($"Ocorreu um erro(Unauthorized) ao enviar o e-mail: {errorItem.message}");
    }
}