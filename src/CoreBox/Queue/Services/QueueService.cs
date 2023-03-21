using System.Text;
using CoreBox.Queue.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreBox.Queue.Services;

public class QueueService : IQueueService
{
    public QueueService()
    {
    }

    public void Publish(PublishRequest request)
    {
        using IConnection connection = request.ConnectionFactory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.BasicPublish(
            exchange: request.Publish.Exchange,
            routingKey: request.Publish.RoutingKey,
            basicProperties: request.Publish.BasicProperties,
            body: Encoding.UTF8.GetBytes(request.Publish.Body)
        );
    }

    public void Receive(ReceiveRequest request)
    {
        using var connection = request.ConnectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
            request.Receive.MessageHandler(Encoding.UTF8.GetString(ea.Body.ToArray()));

        channel.BasicConsume(
            queue: request.Receive.QueueName,
            autoAck: request.Receive.AutoAck,
            consumer: consumer
        );

        Console.ReadLine();
    }
}