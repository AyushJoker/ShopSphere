using FluentValidation;
using ShopSphere.InventoryService.Application.DTOs;

namespace ShopSphere.InventoryService.Application.Validators;

public class ReserveStockRequestDtoValidator
    : AbstractValidator<ReserveStockRequestDto>
{
    public ReserveStockRequestDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}