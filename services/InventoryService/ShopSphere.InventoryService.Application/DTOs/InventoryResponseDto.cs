namespace ShopSphere.InventoryService.Application.DTOs;

public class InventoryResponseDto
{
    public Guid ProductId { get; set; }

    public int AvailableQuantity { get; set; }

    public int ReservedQuantity { get; set; }
}