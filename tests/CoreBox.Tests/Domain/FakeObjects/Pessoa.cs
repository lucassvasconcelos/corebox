using System;
using CoreBox.Domain;

namespace CoreBox.Tests
{
    public class Pessoa : Entity<Pessoa>
    {
        public Pessoa(Guid? id = null)
        {
        }

        public string Nome { get; private set; }
        public Documento Documento { get; private set; }

        public static Pessoa Criar(string nome, string numeroDocumento, DateTime dataEmissao)
        {
            Pessoa p = new Pessoa();
            p.Nome = nome;
            p.Documento = Documento.Criar(numeroDocumento, dataEmissao);
            return p;
        }

        public static Pessoa CriarComId(Guid id, string nome, string numeroDocumento, DateTime dataEmissao)
        {
            Pessoa p = new Pessoa(id);
            p.Nome = nome;
            p.Documento = Documento.Criar(numeroDocumento, dataEmissao);
            return p;
        }
    }
}