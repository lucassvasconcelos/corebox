using System.IO;
using System.Linq;
using CoreBox.Extensions;
using CoreBox.Notification;
using CoreBox.Notification.Abstractions;
using CoreBox.Tests.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using Xunit;

namespace CoreBox.Tests.Notification;

public class DependencyInjectionUnitTests
{
    [Theory, AutoMoqData]
    public void Deve_Injetar_Os_Servicos_De_Email(ServiceCollection services)
    {
        var env = new HostEnvironment();
        env.ContentRootPath = Path.GetFullPath("../../../");
        env.EnvironmentName = "";
        
        var config = (new ConfigurationBuilder().Build()).GetEnvironmentConfiguration(env);
        services.AddSendGridService(config);

        services.Count.Should().BeGreaterThan(default);
        services.Any(a => a.ServiceType.Name == nameof(ISmtpService)).Should().BeTrue();
        services.Any(a => a.ServiceType.Name == nameof(ISendGridClient)).Should().BeTrue();
    }
}