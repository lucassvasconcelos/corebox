using System.Diagnostics.CodeAnalysis;

namespace CoreBox.Queue.Shared;

[ExcludeFromCodeCoverageAttribute]
public class ReceiveRequest
{
    public string QueueName { get; set; }
    public bool AutoAck { get; set; }
    public Action<string> MessageHandler { get; set; }

}