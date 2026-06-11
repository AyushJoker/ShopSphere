namespace ShopSphere.InventoryService.Application.DTOs;

public class AddStockRequestDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}