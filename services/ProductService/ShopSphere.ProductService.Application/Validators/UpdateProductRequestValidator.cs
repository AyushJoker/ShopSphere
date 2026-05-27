using FluentValidation;
using ShopSphere.ProductService.Application.DTOs;

namespace ShopSphere.ProductService.Application.Validators;

public class UpdateProductRequestValidator
    : AbstractValidator<UpdateProductRequestDto>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}