using CoreBox.Queue.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoreBox.Queue;

public static class DependencyInjection
{
    public static void AddQueue(this IServiceCollection services) =>
        services.AddTransient<IQueueService, QueueService>();
}