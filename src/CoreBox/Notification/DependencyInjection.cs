using CoreBox.Notification.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

namespace CoreBox.Notification;

public static class DependencyInjection
{
    public static void AddSendGridService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSendGrid(opt => opt.ApiKey = configuration["SendGrid:APIKey"]);
        services.AddTransient<ISmtpService, SendGridService>();
    }
}