using System;
using System.Security.Cryptography;
using CoreBox.Exceptions;
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
        [InlineData("111.111.111-11", false)]
        [InlineData("11111111111", false)]
        [InlineData("222.222.222-22", false)]
        [InlineData("22222222222", false)]
        [InlineData("333.333.333-33", false)]
        [InlineData("33333333333", false)]
        [InlineData("444.444.444-44", false)]
        [InlineData("44444444444", false)]
        [InlineData("555.555.555-55", false)]
        [InlineData("55555555555", false)]
        [InlineData("666.666.666-66", false)]
        [InlineData("66666666666", false)]
        [InlineData("777.777.777-77", false)]
        [InlineData("77777777777", false)]
        [InlineData("888.888.888-88", false)]
        [InlineData("88888888888", false)]
        [InlineData("999.999.999-99", false)]
        [InlineData("99999999999", false)]
        [InlineData("000.000.000-00", false)]
        [InlineData("00000000000", false)]
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
        [InlineData("11.111.111/1111-11", false)]
        [InlineData("11111111111111", false)]
        [InlineData("22.222.222/2222-22", false)]
        [InlineData("22222222222222", false)]
        [InlineData("33.333.333/3333-33", false)]
        [InlineData("33333333333333", false)]
        [InlineData("44.444.444/4444-44", false)]
        [InlineData("44444444444444", false)]
        [InlineData("55.555.555/5555-55", false)]
        [InlineData("55555555555555", false)]
        [InlineData("66.666.666/6666-66", false)]
        [InlineData("66666666666666", false)]
        [InlineData("77.777.777/7777-77", false)]
        [InlineData("77777777777777", false)]
        [InlineData("88.888.888/8888-88", false)]
        [InlineData("88888888888888", false)]
        [InlineData("99.999.999/9999-99", false)]
        [InlineData("99999999999999", false)]
        [InlineData("00.000.000/0000-00", false)]
        [InlineData("00000000000000", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ValidarCnpj(string cnpj, bool resultado)
            => cnpj.IsCnpj().Should().Be(resultado);

        [Fact]
        public void DeveValidarSeAsSenhasEncryptadasSeraoIguais()
        {
            var result1 = "P@ssw0rd".Encrypt("email1@gmail.com", "s4Itk3y");
            var result2 = "P@ssw0rd".Encrypt("email1@gmail.com", "s4Itk3y");
            (result1 == result2).Should().BeTrue();
        }

        [Fact]
        public void DeveValidarSeAsSenhasEncryptadasSeraoDiferentes()
        {
            var result1 = "P@ssw0rd".Encrypt("email1@gmail.com", "s4Itk3y");
            var result2 = "P@ssw0rd".Encrypt("email2@gmail.com", "s4Itk3y");
            (result1 == result2).Should().BeFalse();
        }

        [Fact]
        public void DeveDecriptarUmaSenha()
        {
            var password = "P@ssw0rd";
            var passwordHash = password.Encrypt("email1@gmail.com", "s4Itk3y");
            var result = passwordHash.Decrypt("email1@gmail.com", "s4Itk3y");
            (result == password).Should().BeTrue();
        }

        [Fact]
        public void DeveNaoDecriptarUmaSenha()
        {
            var password = "P@ssw0rd";
            var passwordHash = password.Encrypt("email1@gmail.com", "s4Itk3y");
            Action act = () => passwordHash.Decrypt("email2@gmail.com", "s4Itk3y");
            act.Should().ThrowExactly<CryptographicException>().WithMessage("Não foi possível decodificar a senha com os dados informados");
        }
    }
}