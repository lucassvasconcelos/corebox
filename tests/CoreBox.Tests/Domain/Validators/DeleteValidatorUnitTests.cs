using System;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests;

public class DeleteValidatorUnitTests
{
    public static readonly Validation ValidEntity = Validation.Criar(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, null, null, null, true);
    public static readonly object[][] TestData =
    {
        new object[] { ValidEntity, "Id é obrigatório!", true, Guid.Empty, DateTime.UtcNow, Guid.NewGuid(), null, null, true, DateTime.UtcNow, Guid.NewGuid() },
        new object[] { ValidEntity, "Data de criação é inválida!", true, Guid.NewGuid(), DateTime.MaxValue, Guid.NewGuid(), null, null, true, DateTime.UtcNow, Guid.NewGuid() },
        new object[] { ValidEntity, "Id do usuário de criação é obrigatório!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.Empty, null, null, true, DateTime.UtcNow, Guid.NewGuid() },
        new object[] { ValidEntity, "Data de exclusão é obrigatória!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, true, null, Guid.NewGuid() },
        new object[] { ValidEntity, "Data de exclusão é inválida!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, true, DateTime.MaxValue, Guid.NewGuid() },
        new object[] { ValidEntity, "Id do usuário de exclusão é obrigatório!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, true, DateTime.UtcNow, null },
        new object[] { ValidEntity, "Flag de exclusão deve ser verdadeira!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, false, DateTime.UtcNow, Guid.NewGuid() },
        new object[] { ValidEntity, "", false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, true, null, null },
        new object[] { ValidEntity, "", false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), true, null, null },
        new object[] { ValidEntity, "", false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), true, null, null },
        new object[] { ValidEntity, "", false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), true, null, null }
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
        Action act = () => entity.Deletar(id, dataCriacao, idUsuarioCriacao, dataUltimaAtualizacao, idUsuarioAtualizacao, foiExcluido, dataExclusao, idUsuarioExclusao, doSoftDeleteValidations);
        
        if (doSoftDeleteValidations)
            act.Should().ThrowExactly<ValidationException>().Where(w => w.Message.Contains(errorMessage) && w.Errors.Count() == 1);
        else
            act.Should().NotThrow();
    }
}