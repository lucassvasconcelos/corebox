using System;

namespace CoreBox
{
    public abstract class DataModel<T, TKey> where T : DataModel<T, TKey>
    {
        public TKey Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}