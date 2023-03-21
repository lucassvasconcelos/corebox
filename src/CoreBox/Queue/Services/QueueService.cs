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
        ValidatePublishRequest(request);

        using IConnection connection = request.ConnectionFactory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.BasicPublish(
            exchange: request.Publish.Exchange,
            routingKey: request.Publish.RoutingKey,
            basicProperties: request.Publish.BasicProperties,
            body: Encoding.UTF8.GetBytes(request.Publish.Body)
        );
    }

    private static void ValidatePublishRequest(PublishRequest request)
    {
        if (request is null)
            throw new ArgumentException("O campo requisição é obrigatório");

        if (request.Publish is null)
            throw new ArgumentException("As configurações de publicação na fila são obrigatórias");

        if (request.ConnectionFactory is null)
            throw new ArgumentException("As configurações de conexão com a fila são obrigatórias");
    }

    public void Receive(ReceiveRequest request)
    {
        ValidateReceiveRequest(request);

        using IConnection connection = request.ConnectionFactory.CreateConnection();
        using IModel channel = connection.CreateModel();

        EventingBasicConsumer consumer = new(channel);

        consumer.Received += (model, ea) =>
            request.Receive.MessageHandler(Encoding.UTF8.GetString(ea.Body.ToArray()));

        channel.BasicConsume(
            queue: request.Receive.QueueName,
            autoAck: request.Receive.AutoAck,
            consumer: consumer
        );

        Console.ReadLine();
    }

    private static void ValidateReceiveRequest(ReceiveRequest request)
    {
        if (request is null)
            throw new ArgumentException("O campo requisição é obrigatório");

        if (request.ConnectionFactory is null)
            throw new ArgumentException("As configurações de conexão com a fila são obrigatórias");

        if (request.Receive is null)
            throw new ArgumentException("As configurações de escuta da fila são obrigatórias");
    }
}