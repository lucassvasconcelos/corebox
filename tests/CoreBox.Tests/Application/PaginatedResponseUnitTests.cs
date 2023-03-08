using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Application;

public class PaginatedResponseUnitTests
{
    [Fact]
    public void Deve_Gerar_Uma_Instancia_De_Uma_Query_Em_Lista()
    {
        PaginatedResponseExample response = new();
        response.Count.Should().Be(0);
        response.Items.Should().BeNullOrEmpty();
    }
}