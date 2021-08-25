using FluentAssertions;
using Xunit;

namespace CoreBox.UnitTests
{
    public class AutoMoqDataAttributeUnitTests
    {
        [Fact]
        public void Deve_Criar_Um_AutoMoqDataAttribute()
        {
            var data = new AutoMoqDataAttribute();
            data.Should().NotBeNull();
        }
    }
}