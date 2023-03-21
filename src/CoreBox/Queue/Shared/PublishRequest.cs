using RabbitMQ.Client;

namespace CoreBox.Queue.Shared;

public class PublishRequest
{
    public IConnectionFactory ConnectionFactory { get; set; }
    public Publish Publish { get; set; }
}