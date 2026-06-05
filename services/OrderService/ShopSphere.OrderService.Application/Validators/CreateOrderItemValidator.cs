using FluentValidation;
using ShopSphere.OrderService.Application.DTOs;

namespace ShopSphere.OrderService.Application.Validators;

public class CreateOrderItemValidator
    : AbstractValidator<CreateOrderItemDto>
{
    public CreateOrderItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}