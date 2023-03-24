using RabbitMQ.Client;

namespace CoreBox.Queue.Shared;

public class PublishRequest
{
    public string Exchange { get; set; }
    public string RoutingKey { get; set; }
    public IBasicProperties BasicProperties { get; set; }
    public string Message { get; set; }
}