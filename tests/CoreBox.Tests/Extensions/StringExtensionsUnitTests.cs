using CoreBox.Extensions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class StringExtensionsUnitTests
    {
        [Theory]
        [InlineData("foo@gmail.com", true)]
        [InlineData("bar@outlook.com", true)]
        [InlineData("bar@teste", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ValidarEmail(string email, bool resultado)
            => email.IsEmail().Should().Be(resultado);

        [Theory]
        [InlineData("436.828.480-10", "43682848010")]
        [InlineData("'!@#$5%¨&*()_-=+*/|", "5")]
        [InlineData("", "")]
        public void ValidarSaneamentoDeSimbolos(string email, string resultado)
            => email.OnlyStringNumbers().Should().Be(resultado);

        [Theory]
        [InlineData("123.456.789-09", true)]
        [InlineData("12345678909", true)]
        [InlineData("436.828.480-10", true)]
        [InlineData("43682848010", true)]
        [InlineData("336.118.480-10", false)]
        [InlineData("33611848010", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ValidarCpf(string cpf, bool resultado)
            => cpf.IsCpf().Should().Be(resultado);

        [Theory]
        [InlineData("15.287.312/0001-03", true)]
        [InlineData("15287312000103", true)]
        [InlineData("66.355.887/0001-49", true)]
        [InlineData("66355887000149", true)]
        [InlineData("66.955.887/0001-49", false)]
        [InlineData("66955887000149", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ValidarCnpj(string cnpj, bool resultado)
            => cnpj.IsCnpj().Should().Be(resultado);
    }
}