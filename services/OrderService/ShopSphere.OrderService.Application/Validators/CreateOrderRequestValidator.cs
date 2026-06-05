using FluentValidation;
using ShopSphere.OrderService.Application.DTOs;

namespace ShopSphere.OrderService.Application.Validators;

public class CreateOrderRequestValidator
    : AbstractValidator<CreateOrderRequestDto>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty();

        RuleForEach(x => x.Items).SetValidator(new CreateOrderItemValidator());
    }
}