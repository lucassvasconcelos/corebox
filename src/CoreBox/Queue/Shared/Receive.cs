using RabbitMQ.Client.Events;

namespace CoreBox.Queue.Shared;

public class Receive
{
    public string QueueName { get; set; }
    public bool AutoAck { get; set; }
    public Action<string> MessageHandler { get; set; }
}