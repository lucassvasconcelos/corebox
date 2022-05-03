using System;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests;

public class CreateValidatorUnitTests
{
    public static readonly object[][] TestData =
    {
        new object[] { "Id é obrigatório!", true, Guid.Empty, DateTime.UtcNow, Guid.NewGuid(), null, null, false, null, null, "..." },
        new object[] { "Data de criação é inválida!", true, Guid.NewGuid(), DateTime.MinValue, Guid.NewGuid(), null, null, false, null, null, "..." },
        new object[] { "Id do usuário de criação é obrigatório!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.Empty, null, null, false, null, null, "..." },
        new object[] { "Data da última atualização deve ser nula no cadastro!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), DateTime.UtcNow, null, false, null, null, "..." },
        new object[] { "Id do usuário da atualização deve ser nulo no cadastro!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, Guid.NewGuid(), false, null, null, "..." },
        new object[] { "Data de exclusão deve ser nula no cadastro!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, false, DateTime.UtcNow, null, "..." },
        new object[] { "Id do usuário da exclusão deve ser nulo no cadastro!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, false, null, Guid.NewGuid(), "..." },
        new object[] { "Flag se foi excluído sempre precisar ser falsa na criação!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, true, null, null, "..." },
        new object[] { string.Empty, false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, false, DateTime.UtcNow, null, "..." },
        new object[] { string.Empty, false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, false, null, Guid.NewGuid(), "..." },
        new object[] { string.Empty, false, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, true, null, null, "..." },
        new object[] { "Campo obrigatório!", true, Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), null, null, false, null, null, "" }
    };

    [Theory, MemberData(nameof(TestData))]
    public void Deve_Efetuar_As_Validacoes_De_Criacao_De_Uma_Entidade(
        string errorMessage,
        bool doSoftDeleteValidations,
        Guid? id,
        DateTime? dataCriacao,
        Guid? idUsuarioCriacao,
        DateTime? dataUltimaAtualizacao,
        Guid? idUsuarioAtualizacao,
        bool? foiExcluido,
        DateTime? dataExclusao,
        Guid? idUsuarioExclusao,
        string campoObrigatorio
    )
    {
        Action act = () => Validation.Criar(id, dataCriacao, idUsuarioCriacao, dataUltimaAtualizacao, idUsuarioAtualizacao, foiExcluido, dataExclusao, idUsuarioExclusao, doSoftDeleteValidations, campoObrigatorio);
        
        if (doSoftDeleteValidations)
            act.Should().ThrowExactly<ValidationException>().Where(w => w.Message.Contains(errorMessage) && w.Errors.Count() == 1);
        else
            act.Should().NotThrow();
    }
}