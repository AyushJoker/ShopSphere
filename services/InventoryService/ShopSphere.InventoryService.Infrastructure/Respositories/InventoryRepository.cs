using Microsoft.EntityFrameworkCore;
using ShopSphere.InventoryService.Application.Interfaces;
using ShopSphere.InventoryService.Domain.Entities;
using ShopSphere.InventoryService.Infrastructure.Data;

namespace ShopSphere.InventoryService.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventoryDbContext _context;

    public InventoryRepository(
        InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<Inventory?> GetByProductIdAsync(Guid productId)
    {
        return await _context.Inventories
            .FirstOrDefaultAsync(
                x => x.ProductId == productId);
    }

    public async Task AddAsync( Inventory inventory)
    {
        await _context.Inventories
            .AddAsync(inventory);
    }

    public Task UpdateAsync(Inventory inventory)
    {
        _context.Inventories.Update(inventory);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}