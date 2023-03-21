using CoreBox.Queue.Shared;

namespace CoreBox.Queue.Services;

public interface IQueueService
{
    void Publish(PublishRequest request);
    void Receive(ReceiveRequest request);
}