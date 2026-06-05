using ShopSphere.OrderService.Domain.Enums;

namespace ShopSphere.OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    public ICollection<OrderItem> Items { get; set; }
        = new List<OrderItem>();
}