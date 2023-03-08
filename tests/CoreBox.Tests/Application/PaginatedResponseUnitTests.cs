using CoreBox.Application;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Application;

public class PaginatedResponseUnitTests
{
    [Fact]
    public void Deve_Gerar_Uma_Instancia_De_Uma_Resposta_Paginada()
    {
        PaginatedResponseExample response = new();
        response.Count.Should().Be(0);
        response.Items.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Deve_Gerar_Uma_Instancia_De_Uma_Resposta_Paginada_Pela_Fabrica()
    {
        var response = PaginatedResponse<PaginatedResponseExample>.Create();
        response.Count.Should().Be(0);
        response.Items.Should().BeNullOrEmpty();
    }
}