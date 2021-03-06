namespace CoreBox.Domain;

public abstract class Entity<T> where T : Entity<T>
{
    public Guid Id { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public Guid IdUsuarioCriacao { get; protected set; }
    public DateTime? DataUltimaAtualizacao { get; protected set; }
    public Guid? IdUsuarioAtualizacao { get; protected set; }
    public DateTime? DataExclusao { get; protected set; }
    public Guid? IdUsuarioExclusao { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity<T>;

        if (ReferenceEquals(this, compareTo))
            return true;

        if (ReferenceEquals(null, compareTo))
            return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity<T> a, Entity<T> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<T> a, Entity<T> b)
        => !(a == b);

    public override int GetHashCode()
        => (GetType().GetHashCode() * 907) + Id.GetHashCode();

    public override string ToString()
        => $"{GetType().Name} [Id = {Id}]";
}