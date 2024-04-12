using System;
using CoreBox.Domain;

namespace CoreBox.Tests
{
    public class Documento : ValueObject<Documento>
    {
        public string Numero { get; private set; }
        public DateTime DataEmissao { get; private set; }

        public static Documento Criar(string numero, DateTime dataEmissao)
            => new()
            {
                Numero = numero,
                DataEmissao = dataEmissao
            };
    }
}