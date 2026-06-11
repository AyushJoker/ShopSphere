using ShopSphere.InventoryService.Domain.Entities;

namespace ShopSphere.InventoryService.Application.Interfaces;

public interface IInventoryRepository
{
    Task<Inventory?> GetByProductIdAsync( Guid productId);

    Task AddAsync(Inventory inventory);

    Task UpdateAsync(Inventory inventory);

    Task SaveChangesAsync();
}