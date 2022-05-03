using System;
using CoreBox.Domain;
using CoreBox.Domain.Validators;
using CoreBox.Extensions;

namespace CoreBox.Tests
{
    public class Validation : Entity<Validation>
    {
        public string CampoObrigatorio { get; set; }

        public static Validation Criar(
            Guid? id = null,
            DateTime? dataCriacao = null,
            Guid? idUsuarioCriacao = null,
            DateTime? dataUltimaAtualizacao = null,
            Guid? idUsuarioAtualizacao = null,
            bool? foiExcluido = null,
            DateTime? dataExclusao = null,
            Guid? idUsuarioExclusao = null,
            bool doSoftDeleteValidations = true,
            string campoObrigatorio = null
        )
        {
            Validation validation = new();
            
            if (id.HasValue) validation.Id = id.Value;
            if (dataCriacao.HasValue) validation.DataCriacao = dataCriacao.Value;
            if (idUsuarioCriacao.HasValue) validation.IdUsuarioCriacao = idUsuarioCriacao.Value;
            validation.DataUltimaAtualizacao = dataUltimaAtualizacao;
            validation.IdUsuarioAtualizacao = idUsuarioAtualizacao;
            if (foiExcluido.HasValue) validation.FoiExcluido = foiExcluido.Value;
            validation.DataExclusao = dataExclusao;
            validation.IdUsuarioExclusao = idUsuarioExclusao;
            validation.CampoObrigatorio = campoObrigatorio;
            
            validation.ValidateAndThrow(new CreateValidator<Validation>(new CustomValidator(), doSoftDeleteValidations));
            return validation;
        }

        public void Atualizar(
            Guid? id = null,
            DateTime? dataCriacao = null,
            Guid? idUsuarioCriacao = null,
            DateTime? dataUltimaAtualizacao = null,
            Guid? idUsuarioAtualizacao = null,
            bool? foiExcluido = null,
            DateTime? dataExclusao = null,
            Guid? idUsuarioExclusao = null,
            bool doSoftDeleteValidations = true,
            string campoObrigatorio = null
        )
        {
            if (id.HasValue) Id = id.Value;
            if (dataCriacao.HasValue) DataCriacao = dataCriacao.Value;
            if (idUsuarioCriacao.HasValue) IdUsuarioCriacao = idUsuarioCriacao.Value;
            DataUltimaAtualizacao = dataUltimaAtualizacao;
            IdUsuarioAtualizacao = idUsuarioAtualizacao;
            if (foiExcluido.HasValue) FoiExcluido = foiExcluido.Value;
            DataExclusao = dataExclusao;
            IdUsuarioExclusao = idUsuarioExclusao;
            CampoObrigatorio = campoObrigatorio;

            this.ValidateAndThrow(new UpdateValidator<Validation>(new CustomValidator(), doSoftDeleteValidations));
        }

        public void Deletar(
            Guid? id = null,
            DateTime? dataCriacao = null,
            Guid? idUsuarioCriacao = null,
            DateTime? dataUltimaAtualizacao = null,
            Guid? idUsuarioAtualizacao = null,
            bool? foiExcluido = null,
            DateTime? dataExclusao = null,
            Guid? idUsuarioExclusao = null,
            bool doSoftDeleteValidations = true,
            string campoObrigatorio = null
        )
        {
            if (id.HasValue) Id = id.Value;
            if (dataCriacao.HasValue) DataCriacao = dataCriacao.Value;
            if (idUsuarioCriacao.HasValue) IdUsuarioCriacao = idUsuarioCriacao.Value;
            DataUltimaAtualizacao = dataUltimaAtualizacao;
            IdUsuarioAtualizacao = idUsuarioAtualizacao;
            if (foiExcluido.HasValue) FoiExcluido = foiExcluido.Value;
            DataExclusao = dataExclusao;
            IdUsuarioExclusao = idUsuarioExclusao;
            CampoObrigatorio = campoObrigatorio;

            this.ValidateAndThrow(new DeleteValidator<Validation>(new CustomValidator(), doSoftDeleteValidations));
        }
    }
}