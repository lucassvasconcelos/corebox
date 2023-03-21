using RabbitMQ.Client;

namespace CoreBox.Queue.Shared;

public class ReceiveRequest
{
    public IConnectionFactory ConnectionFactory { get; set; }
    public Receive Receive { get; set; }

}