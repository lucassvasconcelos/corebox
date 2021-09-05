using System;
using CoreBox.Domain;

namespace CoreBox.Tests
{
    public class Documento : ValueObject<Documento>
    {
        public string Numero { get; private set; }
        public DateTime DataEmissao { get; private set; }

        protected override bool EqualsCore(Documento other)
            => Numero == other.Numero;

        protected override int GetHashCodeCore()
            => (GetType().GetHashCode() * 2) + Numero.GetHashCode();

        public static Documento Criar(string numero, DateTime dataEmissao)
        {
            Documento d = new Documento();
            d.Numero = numero;
            d.DataEmissao = dataEmissao;
            return d;
        }
    }
}