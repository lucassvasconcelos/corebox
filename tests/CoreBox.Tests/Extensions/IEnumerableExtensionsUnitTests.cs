using System.Collections.Generic;
using System.Linq;
using CoreBox.Extensions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class IEnumerableExtensionsUnitTests
    {
        [Fact]
        public void Deve_Obter_Uma_Enumeracao_Vazia()
        {
            IEnumerable<string> strings = null;

            strings = strings.OrEmpty<string>();

            strings.Should().BeEmpty();
        }

        [Fact]
        public void Deve_Obter_A_Mesma_Enumeracao()
        {
            IEnumerable<string> strings = new List<string>{ "Teste" };

            strings = strings.OrEmpty<string>();

            strings.Should().HaveCount(1);
            strings.First().Should().Be("Teste");
        }
    }
}