using System.Collections.Generic;
using System.Linq;
using CoreBox.Specification;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Specification
{
    public class SpecificationUnitTests
    {
        private readonly List<Pessoa> _pessoas;

        public SpecificationUnitTests()
        {
            _pessoas = new List<Pessoa>
            {
                new Pessoa("João", 10, "M", false),
                new Pessoa("Miguel", 10, "M", true),
                new Pessoa("Laura", 8, "F", false),
                new Pessoa("Helena", 8, "F", false),
                new Pessoa("Lucas", 28, "M", false),
                new Pessoa("Cíntia", 32, "F", false),
                new Pessoa("Bruna", 20, "F", false),
                new Pessoa("Rita", 56, "F", false),
                new Pessoa("Sergio", 62, "M", false),
                new Pessoa("Marcos", 40, "M", true)
            };
        }

        [Fact]
        public void Deve_Obter_Pessoas_Com_Deficiencia()
        {
            var pessoaComDeficiencia = new PessoaComDeficienciaSpecification();

            var pessoasComDeficiencia = _pessoas.Where(pessoa
                => pessoaComDeficiencia.IsSatisfiedBy(pessoa)).ToList();

            pessoasComDeficiencia.Count().Should().Be(2);
        }

        [Fact]
        public void Deve_Obter_Pessoas_Menores_De_Idade_Do_Sexo_Masculino()
        {
            var pessoaMenorIdade = new PessoaMenorDeIdadeSpecification();
            var pessoaSexoMasculino = new PessoaSexoMasculinoSpecification();

            var pessoasMenoresDeIdadeDoSexoMasculino = _pessoas.Where(pessoa
                => pessoaMenorIdade.And(pessoaSexoMasculino).IsSatisfiedBy(pessoa)).ToList();

            pessoasMenoresDeIdadeDoSexoMasculino.Count().Should().Be(2);
        }

        [Fact]
        public void Deve_Obter_Pessoas_Menores_De_Idade_Do_Sexo_Masculino_Ou_Feminino()
        {
            var pessoaMenorIdade = new PessoaMenorDeIdadeSpecification();
            var pessoaSexoMasculino = new PessoaSexoMasculinoSpecification();
            var pessoaSexoFeminino = new PessoaSexoFemininoSpecification();

            var pessoasMenoresDeIdadeDoSexoMasculino = _pessoas.Where(pessoa
                => pessoaMenorIdade.And(pessoaSexoMasculino.Or(pessoaSexoFeminino)).IsSatisfiedBy(pessoa)).ToList();

            pessoasMenoresDeIdadeDoSexoMasculino.Count().Should().Be(4);
        }

        [Fact]
        public void Deve_Obter_Pessoas_Nao_Idosas()
        {
            var pessoaIdosa = new PessoaIdosaSpecification();

            var pessoasNaoIdosas = _pessoas.Where(pessoa
                => pessoaIdosa.Not().IsSatisfiedBy(pessoa)).ToList();

            pessoasNaoIdosas.Count().Should().Be(9);
        }

        [Fact]
        public void Deve_Obter_Pessoas_Maiores_De_Idade_Do_Sexo_Feminino_Atraves_Do_Specification_Builder()
        {
            var spec = Specification<Pessoa>.All;
            spec = spec.And((new PessoaMenorDeIdadeSpecification()).Not());
            spec = spec.And(new PessoaSexoFemininoSpecification());

            var mulheresMaioresDeIdade = _pessoas.Where(pessoa
                => spec.IsSatisfiedBy(pessoa)).ToList();

            mulheresMaioresDeIdade.Count().Should().Be(3);
        }

        [Fact]
        public void Deve_Obter_Pessoas_De_Qualquer_Sexo_Atraves_Do_Specification_Builder()
        {
            var spec = Specification<Pessoa>.All;
            spec = spec.Or(new PessoaSexoFemininoSpecification());
            spec = spec.Or(new PessoaSexoMasculinoSpecification());

            var pessoasDeQualquerSexo = _pessoas.Where(pessoa
                => spec.IsSatisfiedBy(pessoa)).ToList();

            pessoasDeQualquerSexo.Count().Should().Be(10);
        }

        [Fact]
        public void Deve_Obter_Pessoas_De_Qualquer_Sexo_E_Que_Possuem_Deficiencia_Atraves_Do_Specification_Builder()
        {
            var spec = Specification<Pessoa>.All;
            spec = spec.Or(new PessoaSexoFemininoSpecification());
            spec = spec.Or(new PessoaSexoMasculinoSpecification());

            var pcd = new PessoaComDeficienciaSpecification();

            var pessoasComDeficiencia = _pessoas.Where(pessoa
                => pcd.And(spec).IsSatisfiedBy(pessoa)).ToList();

            pessoasComDeficiencia.Count().Should().Be(2);
        }
    }
}