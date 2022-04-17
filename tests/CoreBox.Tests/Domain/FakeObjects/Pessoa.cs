using System;
using CoreBox.Domain;

namespace CoreBox.Tests
{
    public class Pessoa : Entity<Pessoa>
    {
        public string Nome { get; private set; }
        public Documento Documento { get; private set; }

        public static Pessoa Criar(string nome, string numeroDocumento, DateTime dataEmissao)
        {
            Pessoa p = new Pessoa();
            p.Nome = nome;
            p.Documento = Documento.Criar(numeroDocumento, dataEmissao);
            p.IdUsuarioCriacao = Guid.NewGuid();
            return p;
        }
    }
}