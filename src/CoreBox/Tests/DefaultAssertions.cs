using CoreBox.Domain;
using FluentAssertions;

namespace CoreBox.Test;

public static class DefaultAssertions<T> where T : Entity<T>
{
    public static void AssertCreation(T entity)
    {
        entity.Id.Should().NotBeEmpty();
        entity.DataCriacao.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(2));
        entity.IdUsuarioCriacao.Should().NotBeEmpty();
        entity.DataUltimaAtualizacao.Should().BeNull();
        entity.IdUsuarioAtualizacao.Should().BeNull();
        entity.DataExclusao.Should().BeNull();
        entity.IdUsuarioExclusao.Should().BeNull();
    }

    public static void AssertUpdate(T entity)
    {
        entity.Id.Should().NotBeEmpty();
        entity.DataCriacao.Should().BeLessThan(TimeSpan.FromTicks(entity.DataUltimaAtualizacao.Value.Ticks));
        entity.IdUsuarioCriacao.Should().NotBeEmpty();
        entity.DataUltimaAtualizacao.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(2));
        entity.IdUsuarioAtualizacao.Should().NotBeEmpty();
        entity.DataExclusao.Should().BeNull();
        entity.IdUsuarioExclusao.Should().BeNull();
    }

    public static void AssertDeletion(T entity)
    {
        entity.Id.Should().NotBeEmpty();
        entity.DataCriacao.Should().BeLessThan(TimeSpan.FromTicks(entity.DataExclusao.Value.Ticks));
        entity.IdUsuarioCriacao.Should().NotBeEmpty();
        entity.DataExclusao.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(2));
        entity.IdUsuarioExclusao.Should().NotBeEmpty();
    }
}