using FluentValidation;

namespace CoreBox.Domain.Validators;

public class DeleteValidator<T> : AbstractValidator<T> where T : Entity<T>
{
    public DeleteValidator(AbstractValidator<T> anotherValidator = null, bool doSoftDeleteValidations = true)
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage($"Id é obrigatório!");
        RuleFor(r => r.DataCriacao).LessThan(DateTime.UtcNow).WithMessage($"Data de criação é inválida!");
        RuleFor(r => r.IdUsuarioCriacao).NotEmpty().WithMessage($"Id do usuário de criação é obrigatório!");
        
        if (doSoftDeleteValidations)
        {
            RuleFor(r => r.DataExclusao).NotEmpty().WithMessage($"Data de exclusão é obrigatória!");
            RuleFor(r => r.DataExclusao).InclusiveBetween(DateTime.UtcNow.AddMilliseconds(-2000), DateTime.UtcNow.AddMilliseconds(2000)).WithMessage($"Data de exclusão é inválida!");
            RuleFor(r => r.IdUsuarioExclusao).NotEmpty().WithMessage($"Id do usuário de exclusão é obrigatório!");
        }

        if (anotherValidator is not null)
            Include(anotherValidator);
    }
}