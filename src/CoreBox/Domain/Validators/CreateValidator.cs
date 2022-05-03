using FluentValidation;

namespace CoreBox.Domain.Validators;

public class CreateValidator<T> : AbstractValidator<T> where T : Entity<T>
{
    public CreateValidator(AbstractValidator<T> anotherValidator = null, bool doSoftDeleteValidations = true)
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage($"Id é obrigatório!");
        RuleFor(r => r.DataCriacao).InclusiveBetween(DateTime.UtcNow.AddMilliseconds(-2000), DateTime.UtcNow.AddMilliseconds(2000)).WithMessage($"Data de criação é inválida!");
        RuleFor(r => r.IdUsuarioCriacao).NotEmpty().WithMessage($"Id do usuário de criação é obrigatório!");
        RuleFor(r => r.DataUltimaAtualizacao).Empty().WithMessage($"Data da última atualização deve ser nula no cadastro!");
        RuleFor(r => r.IdUsuarioAtualizacao).Empty().WithMessage($"Id do usuário da atualização deve ser nulo no cadastro!");
        
        if (doSoftDeleteValidations)
        {
            RuleFor(r => r.DataExclusao).Empty().WithMessage($"Data de exclusão deve ser nula no cadastro!");
            RuleFor(r => r.IdUsuarioExclusao).Empty().WithMessage($"Id do usuário da exclusão deve ser nulo no cadastro!");
            RuleFor(r => r.FoiExcluido).Equal(false).WithMessage($"Flag se foi excluído sempre precisar ser falsa na criação!");
        }

        if (anotherValidator is not null)
            Include(anotherValidator);
    }
}