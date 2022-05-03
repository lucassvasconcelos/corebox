using FluentValidation;

namespace CoreBox.Domain.Validators;

public class UpdateValidator<T> : AbstractValidator<T> where T : Entity<T>
{
    public UpdateValidator(AbstractValidator<T> anotherValidator = null, bool doSoftDeleteValidations = true)
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage($"Id é obrigatório!");
        RuleFor(r => r.DataCriacao).LessThan(DateTime.UtcNow).WithMessage($"Data de criação é inválida!");
        RuleFor(r => r.IdUsuarioCriacao).NotEmpty().WithMessage($"Id do usuário de criação é obrigatório!");
        RuleFor(r => r.DataUltimaAtualizacao).NotEmpty().WithMessage($"Data da última atualização é obrigatória!");
        RuleFor(r => r.DataUltimaAtualizacao).InclusiveBetween(DateTime.UtcNow.AddMilliseconds(-2000), DateTime.UtcNow.AddMilliseconds(2000)).WithMessage($"Data da última atualização é inválida!");
        RuleFor(r => r.IdUsuarioAtualizacao).NotEmpty().WithMessage($"Id do usuário da atualização é obrigatório!");
        
        if (doSoftDeleteValidations)
        {
            RuleFor(r => r.DataExclusao).Empty().WithMessage($"Data de exclusão deve ser nula na atualização!");
            RuleFor(r => r.IdUsuarioExclusao).Empty().WithMessage($"Id do usuário da exclusão deve ser nulo na atualização!");
            RuleFor(r => r.FoiExcluido).Equal(false).WithMessage($"Flag se foi excluído sempre precisar ser falsa ao atualizar!");
        }

        if (anotherValidator is not null)
            Include(anotherValidator);
    }
}