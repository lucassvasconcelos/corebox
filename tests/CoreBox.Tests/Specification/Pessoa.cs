using CoreBox.Domain;

namespace CoreBox.Tests.Specification
{
    public class Pessoa : Entity<Pessoa>
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }
        public bool Pcd { get; set; }

        public Pessoa(string nome, int idade, string sexo, bool pcd)
        {
            Nome = nome;
            Idade = idade;
            Sexo = sexo;
            Pcd = pcd;
        }
    }
}