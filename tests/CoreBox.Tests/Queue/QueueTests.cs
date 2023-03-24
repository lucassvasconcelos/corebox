using System;
using System.Threading.Tasks;
using CoreBox.Queue.Services;
using CoreBox.Queue.Shared;
using CoreBox.Tests.Queue.Data;
using FluentAssertions;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace CoreBox.Tests.Queue;

public class QueueTests
{

    [Fact]
    public void Deve_Criar_QueueService()
    {
        IConnectionFactory connectionFactory = new ConnectionFactoryFake();
        using IQueueService queueService = new QueueService(connectionFactory);
        queueService.Should().NotBeNull();
    }

    [Fact]
    public void Nao_Deve_Publicar_Uma_Mensagem_Sem_Informar_A_Requisicao()
    {
        IConnectionFactory connectionFactory = new ConnectionFactoryFake();
        using IQueueService queueService = new QueueService(connectionFactory);

        Func<Task> act = () =>
        {
            queueService.Publish(null);
            return Task.CompletedTask;
        };

        act.Should()
            .ThrowExactlyAsync<ArgumentException>()
            .Where(item =>
                item.Message.Contains("O campo requisição é obrigatório")
            );
    }

    [Fact]
    public void Deve_Enviar_A_Mensagem()
    {
        var modelFake = new Mock<ModelFake>();
        modelFake.Setup(item => item.BasicPublish(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<bool>(),
            It.IsAny<IBasicProperties>(),
            It.IsAny<ReadOnlyMemory<byte>>()
        ));

        var connectionFake = new Mock<ConnectionFake>();
        connectionFake.Setup(item => item.CreateModel()).Returns(modelFake.Object);

        var connectionFactory = new Mock<ConnectionFactoryFake>();
        connectionFactory.Setup(item => item.CreateConnection()).Returns(connectionFake.Object);

        IQueueService queueService = new QueueService(connectionFactory.Object);

        queueService.Publish(
            new PublishRequest
            {
                BasicProperties = null,
                Message = "Message",
                Exchange = string.Empty,
                RoutingKey = "teste"
            }
        );

        connectionFake.Verify(item => item.CreateModel(), Times.Once);
        connectionFactory.Verify(item => item.CreateConnection(), Times.Once);
        modelFake.Verify(item =>
            item.BasicPublish(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<IBasicProperties>(),
                It.IsAny<ReadOnlyMemory<byte>>()
            ),
            Times.Once
        );
    }
}