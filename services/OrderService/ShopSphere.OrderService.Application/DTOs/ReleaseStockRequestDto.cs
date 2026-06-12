namespace ShopSphere.OrderService.Application.DTOs;

public class ReleaseStockRequestDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}