using System.Collections.Generic;
using System.Linq;
using CoreBox.Extensions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class ExpressionExtensionsUnitTests
    {
        private readonly List<string> _nomes = new List<string> { "Maria", "Mariana", "Maria Lúcia", "Maria Clara" };

        [Fact]
        public void Deve_Encontrar_Somente_Nome_Mariana_Dentro_Do_Array_Strings()
        {
            var predicate = ExpressionExtensions.True<string>();
            predicate = predicate.And(p => p == "Mariana");

            var resultado = _nomes.Where(predicate.Compile());
            resultado.Count().Should().Be(1);
            resultado.First().Should().Be("Mariana");
        }

        [Fact]
        public void Deve_Encontrar_Todos_Os_Nomes_Que_Iniciam_Com_Maria()
        {
            var predicate = ExpressionExtensions.True<string>();
            predicate = predicate.And(p => p.StartsWith("Maria"));

            var resultado = _nomes.Where(predicate.Compile());
            resultado.Count().Should().Be(4);
        }

        [Fact]
        public void Nao_Deve_Encontrar_O_Nome_Mariano()
        {
            var predicate = ExpressionExtensions.False<string>();
            predicate = predicate.Or(p => p == "Mariano");

            var resultado = _nomes.Where(predicate.Compile());
            resultado.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Nao_Deve_Encontrar_O_Nome_Mariano_Mas_Deve_Encontrar_MariaClara()
        {
            var predicate = ExpressionExtensions.False<string>();
            predicate = predicate.Or(p => p == "Mariano");
            predicate = predicate.Or(p => p == "Maria Clara");

            var resultado = _nomes.Where(predicate.Compile());
            resultado.Count().Should().Be(1);
            resultado.First().Should().Be("Maria Clara");
        }
    }
}