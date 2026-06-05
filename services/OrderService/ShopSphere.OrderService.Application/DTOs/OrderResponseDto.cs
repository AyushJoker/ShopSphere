using ShopSphere.OrderService.Domain.Enums;

namespace ShopSphere.OrderService.Application.DTOs;

public class OrderResponseDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    public List<OrderItemResponseDto> Items { get; set; }
        = new();
}