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
        IQueueService queueService = new QueueService();
        queueService.Should().NotBeNull();
    }

    [Fact]
    public void Nao_Deve_Publicar_Uma_Mensagem_Sem_Informar_A_Requisicao()
    {
        IQueueService queueService = new QueueService();

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

    [Theory]
    [ClassData(typeof(InvalidRequest))]
    public void Nao_Deve_Publicar_Uma_Mensagem_Com_Requisicao_Invalida(ConnectionFactory connectionFactory, Publish publish, string error)
    {
        IQueueService queueService = new QueueService();

        Func<Task> act = () =>
        {
            queueService.Publish(
                new PublishRequest
                {
                    ConnectionFactory = connectionFactory,
                    Publish = publish
                }
            );
            return Task.CompletedTask;
        };

        act.Should()
            .ThrowExactlyAsync<ArgumentException>()
            .Where(item =>
                item.Message.Contains(error)
            );
    }

    [Fact]
    public void Deve_Enviar_A_Mensagem()
    {
        IQueueService queueService = new QueueService();

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

        queueService.Publish(
            new PublishRequest
            {
                ConnectionFactory = connectionFactory.Object,
                Publish = new Publish
                {
                    BasicProperties = null,
                    Body = "Message",
                    Exchange = string.Empty,
                    RoutingKey = "teste"
                }
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