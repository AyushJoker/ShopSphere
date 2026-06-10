namespace ShopSphere.InventoryService.Domain.Entities;

public class Inventory
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int AvailableQuantity { get; set; }

    public int ReservedQuantity { get; set; }

    public DateTime LastUpdated { get; set; }
}