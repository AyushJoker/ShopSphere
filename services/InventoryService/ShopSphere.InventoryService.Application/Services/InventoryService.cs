using ShopSphere.InventoryService.Application.DTOs;
using ShopSphere.InventoryService.Application.Interfaces;
using ShopSphere.InventoryService.Domain.Entities;

namespace ShopSphere.InventoryService.Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryService(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<InventoryResponseDto> AddStockAsync(AddStockRequestDto request)
    {
        var inventory =
            await _inventoryRepository
                .GetByProductIdAsync(
                    request.ProductId);

        if (inventory is null)
        {
            inventory = new Inventory
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                AvailableQuantity = request.Quantity,
                ReservedQuantity = 0,
                LastUpdated = DateTime.UtcNow
            };

            await _inventoryRepository
                .AddAsync(inventory);
        }
        else
        {
            inventory.AvailableQuantity +=
                request.Quantity;

            inventory.LastUpdated =
                DateTime.UtcNow;

            await _inventoryRepository
                .UpdateAsync(inventory);
        }

        await _inventoryRepository
            .SaveChangesAsync();

        return new InventoryResponseDto
        {
            ProductId = inventory.ProductId,
            AvailableQuantity =
                inventory.AvailableQuantity,
            ReservedQuantity =
                inventory.ReservedQuantity
        };
    }

    public async Task<InventoryResponseDto?>GetInventoryAsync(Guid productId)
    {
        var inventory =
            await _inventoryRepository
                .GetByProductIdAsync(productId);

        if (inventory is null)
        {
            return null;
        }

        return new InventoryResponseDto
        {
            ProductId = inventory.ProductId,
            AvailableQuantity =
                inventory.AvailableQuantity,
            ReservedQuantity =
                inventory.ReservedQuantity
        };
    }
}