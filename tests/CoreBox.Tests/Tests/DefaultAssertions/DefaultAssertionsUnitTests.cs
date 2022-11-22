using System;
using CoreBox.Test;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests
{
    public class DefaultAssertionsUnitTests
    {
        [Fact]
        public void Deve_Gerar_Uma_Exception_Por_Nao_Informar_O_Usuario_Criador()
        {
            Car car = new();
            
            Action act = () => DefaultAssertions<Car>.AssertCreation(car);

            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Deve_Criar_A_Entidade()
        {
            Car car = new();
            car.CadastrarUsuarioCriacao();
            
            Action act = () => DefaultAssertions<Car>.AssertCreation(car);

            act.Should().NotThrow();
        }

        [Fact]
        public void Deve_Gerar_Uma_Exception_Por_Nao_Informar_O_Usuario_Atualizador_E_Data_Atualizacao()
        {
            Car car = new();
            car.CadastrarUsuarioCriacao();
            
            Action act = () => DefaultAssertions<Car>.AssertUpdate(car);

            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Deve_Atualizar_A_Entidade()
        {
            Car car = new();
            car.CadastrarUsuarioCriacao();
            car.CadastrarUsuarioAtualizacao();
            
            Action act = () => DefaultAssertions<Car>.AssertUpdate(car);

            act.Should().NotThrow();
        }

        [Fact]
        public void Deve_Gerar_Uma_Exception_Por_Nao_Informar_O_Usuario_Da_Exclusao_E_Data_Exclusao()
        {
            Car car = new();
            car.CadastrarUsuarioCriacao();
            
            Action act = () => DefaultAssertions<Car>.AssertDeletion(car);

            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Deve_Excluir_A_Entidade()
        {
            Car car = new();
            car.CadastrarUsuarioCriacao();
            car.CadastrarUsuarioAtualizacao();
            car.CadastrarUsuarioDelecao();
            
            Action act = () => DefaultAssertions<Car>.AssertDeletion(car);

            act.Should().NotThrow();
        }
    }
}