namespace ShopSphere.OrderService.Application.DTOs;

public class ReserveStockRequestDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}