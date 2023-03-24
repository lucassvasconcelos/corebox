using System.Diagnostics.CodeAnalysis;
using System.Text;
using CoreBox.Queue.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreBox.Queue.Services;

public class QueueService : IQueueService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public QueueService(IConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Publish(PublishRequest request)
    {
        if (request is null)
            throw new ArgumentException("O campo requisição é obrigatório");

        _channel.BasicPublish(
            exchange: request.Exchange,
            routingKey: request.RoutingKey,
            basicProperties: request.BasicProperties,
            body: Encoding.UTF8.GetBytes(request.Message)
        );
    }

    [ExcludeFromCodeCoverage]
    public void Receive(ReceiveRequest request)
    {
        if (request is null)
            throw new ArgumentException("O campo requisição é obrigatório");

        EventingBasicConsumer consumer = new(_channel);

        consumer.Received += [ExcludeFromCodeCoverage] (model, ea) =>
            request.MessageHandler(Encoding.UTF8.GetString(ea.Body.ToArray()));

        _channel.BasicConsume(
            queue: request.QueueName,
            autoAck: request.AutoAck,
            consumer: consumer
        );
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}