using System;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests;

public class UpdateValidatorUnitTests
{
    public static readonly Validation ValidEntity = Validation.Criar(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, null, null, null, true);
    public static readonly object[][] TestData =
    {
        new object[] { ValidEntity, "Id é obrigatório!", true, Guid.Empty, DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), false, null, null },
        new object[] { ValidEntity, "Data de criação é inválida!", true, Guid.NewGuid(), DateTime.MaxValue, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), false, null, null },
        new object[] { ValidEntity, "Id do usuário de criação é obrigatório!", true, Guid.NewGuid(), DateTime.UtcNow.AddMinutes(-1), Guid.Empty, DateTime.UtcNow, Guid.NewGuid(), false, null, null },
        new object[] { ValidEntity, "Data da última atualização é obrigatória!", true, Guid.NewGuid(), DateTime.UtcNow.AddMinutes(-1), Guid.NewGuid(), null, Guid.NewGuid(), false, null, null },
        new object[] { ValidEntity, "Data da última atualização é inválida!", true, Guid.NewGuid(), DateTime.UtcNow.AddMinutes(-1), Guid.NewGuid(), DateTime.MinValue, Guid.NewGuid(), false, null, null },
        new object[] { ValidEntity, "Id do usuário da atualização é obrigatório!", true, Guid.NewGuid(), DateTime.UtcNow.AddMinutes(-1), Guid.NewGuid(), DateTime.UtcNow, null, false, null, null },
        new object[] { ValidEntity, "Data de exclusão deve ser nula na atualização!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), false, DateTime.UtcNow, null },
        new object[] { ValidEntity, "Id do usuário da exclusão deve ser nulo na atualização!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), false, null, Guid.NewGuid() },
        new object[] { ValidEntity, "Flag se foi excluído sempre precisar ser falsa ao atualizar!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), true, null, null },
        new object[] { ValidEntity, string.Empty, false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), false, DateTime.UtcNow, Guid.NewGuid() },
        new object[] { ValidEntity, string.Empty, false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), false, DateTime.UtcNow, Guid.NewGuid() },
        new object[] { ValidEntity, string.Empty, false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), true, DateTime.UtcNow, Guid.NewGuid() },
    };

    [Theory, MemberData(nameof(TestData))]
    public void Deve_Efetuar_As_Validacoes_De_Atualizacao_De_Uma_Entidade(
        Validation entity,
        string errorMessage,
        bool doSoftDeleteValidations,
        Guid? id,
        DateTime? dataCriacao,
        Guid? idUsuarioCriacao,
        DateTime? dataUltimaAtualizacao,
        Guid? idUsuarioAtualizacao,
        bool? foiExcluido,
        DateTime? dataExclusao,
        Guid? idUsuarioExclusao
    )
    {
        Action act = () => entity.Atualizar(id, dataCriacao, idUsuarioCriacao, dataUltimaAtualizacao, idUsuarioAtualizacao, foiExcluido, dataExclusao, idUsuarioExclusao, doSoftDeleteValidations);
        
        if (doSoftDeleteValidations)
            act.Should().ThrowExactly<ValidationException>().Where(w => w.Message.Contains(errorMessage) && w.Errors.Count() == 1);
        else
            act.Should().NotThrow();
    }
}