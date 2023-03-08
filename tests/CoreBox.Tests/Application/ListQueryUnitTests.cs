using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Application;

public class ListQueryUnitTests
{
    [Fact]
    public void Deve_Gerar_Uma_Instancia_De_Uma_Query_Em_Lista()
    {
        ListQueryExample query = new();
        query.Descending.Should().BeNull();
        query.Limit.Should().Be(30);
        query.Offset.Should().Be(0);
        query.OrderBy.Should().BeNull();
        query.Term.Should().BeNull();
    }
}