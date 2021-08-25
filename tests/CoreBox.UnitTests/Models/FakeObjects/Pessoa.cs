using System;

namespace CoreBox.UnitTests
{
    public class Pessoa : Entity<Pessoa, Guid>
    {
        public string Nome { get; private set; }
        public Documento Documento { get; private set; }

        public static Pessoa Criar(string nome, string numeroDocumento, DateTime dataEmissao)
        {
            Pessoa p = new Pessoa();
            p.Id = Guid.NewGuid();
            p.DataCriacao = DateTime.Now;
            p.Nome = nome;
            p.Documento = Documento.Criar(numeroDocumento, dataEmissao);
            return p;
        }
    }
}