using FluentValidation;
using FluentValidation.Results;

namespace CoreBox.Tests.Notification;

public class Product
{
    public int Id { get; set; }
    public string Description { get; set; }

    public static (Product product, ValidationResult validationResult) Create(int id, string description)
    {
        Product p = new() { Id = id, Description = description };
        return (p, new ProductValidator().Validate(p));
    }
}

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(r => r.Id)
            .GreaterThan(0)
            .WithMessage("Identificador deve ser maior que zero");

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Descrição requerida!");
    }
}