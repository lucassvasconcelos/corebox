using System;

namespace CoreBox
{
    public abstract class Entity<T, TKey> where T : Entity<T, TKey>
    {
        public TKey Id { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime DataUltimaAtualizacao { get; protected set; }

        protected Entity() { }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T, TKey>;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (ReferenceEquals(null, compareTo))
                return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T, TKey> a, Entity<T, TKey> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T, TKey> a, Entity<T, TKey> b)
            => !(a == b);

        public override int GetHashCode()
            => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString()
            => $"{GetType().Name} [Id = {Id}]";
    }
}