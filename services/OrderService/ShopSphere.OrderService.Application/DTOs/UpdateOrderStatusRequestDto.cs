using ShopSphere.OrderService.Domain.Enums;

namespace ShopSphere.OrderService.Application.DTOs;

public class UpdateOrderStatusRequestDto
{
    public OrderStatus Status { get; set; }
}