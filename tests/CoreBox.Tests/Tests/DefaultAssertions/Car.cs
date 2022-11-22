using System;
using CoreBox.Domain;

namespace CoreBox.Tests
{
    public class Car : Entity<Car>
    {
        public void CadastrarUsuarioCriacao()
            => IdUsuarioCriacao = Guid.NewGuid();

        public void CadastrarUsuarioAtualizacao()
        {
            IdUsuarioAtualizacao = Guid.NewGuid();
            DataUltimaAtualizacao = DateTime.UtcNow;
        }

        public void CadastrarUsuarioDelecao()
        {
            IdUsuarioExclusao = Guid.NewGuid();
            DataExclusao = DateTime.UtcNow;
        }
    }
}