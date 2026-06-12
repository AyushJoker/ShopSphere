using FluentValidation;
using ShopSphere.InventoryService.Application.DTOs;

namespace ShopSphere.InventoryService.Application.Validators;

public class ReleaseStockRequestDtoValidator
    : AbstractValidator<ReleaseStockRequestDto>
{
    public ReleaseStockRequestDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}