namespace ShopSphere.OrderService.Application.DTOs;

public class CreateOrderRequestDto
{
    public List<CreateOrderItemDto> Items { get; set; }
        = new();

    public bool SimulatePaymentFailure { get; set; }
}