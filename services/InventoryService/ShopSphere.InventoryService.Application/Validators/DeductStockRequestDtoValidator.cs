using FluentValidation;
using ShopSphere.InventoryService.Application.DTOs;

namespace ShopSphere.InventoryService.Application.Validators;

public class DeductStockRequestDtoValidator
    : AbstractValidator<DeductStockRequestDto>
{
    public DeductStockRequestDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}