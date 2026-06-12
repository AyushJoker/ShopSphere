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
            throw new NotFoundException($"Inventory not found for product {productId}");
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
    public async Task<InventoryResponseDto> ReserveStockAsync(ReserveStockRequestDto request)
    {
        var inventory =
            await _inventoryRepository
                .GetByProductIdAsync(request.ProductId);

        if (inventory is null)
        {
            throw new NotFoundException(
                $"Inventory not found for product {request.ProductId}");
        }

        if (inventory.AvailableQuantity < request.Quantity)
        {
            throw new BadRequestException(
                "Insufficient stock available");
        }

        inventory.AvailableQuantity -= request.Quantity;
        inventory.ReservedQuantity += request.Quantity;
        inventory.LastUpdated = DateTime.UtcNow;

        await _inventoryRepository.UpdateAsync(inventory);

        await _inventoryRepository.SaveChangesAsync();

        return new InventoryResponseDto
        {
            ProductId = inventory.ProductId,
            AvailableQuantity = inventory.AvailableQuantity,
            ReservedQuantity = inventory.ReservedQuantity
        };
    }
    public async Task<InventoryResponseDto> ReleaseStockAsync(ReleaseStockRequestDto request)
    {
        var inventory =
            await _inventoryRepository
                .GetByProductIdAsync(request.ProductId);

        if (inventory is null)
        {
            throw new NotFoundException(
                $"Inventory not found for product {request.ProductId}");
        }

        if (inventory.ReservedQuantity < request.Quantity)
        {
            throw new BadRequestException(
                "Cannot release more than reserved quantity");
        }

        inventory.AvailableQuantity += request.Quantity;
        inventory.ReservedQuantity -= request.Quantity;
        inventory.LastUpdated = DateTime.UtcNow;

        await _inventoryRepository.UpdateAsync(inventory);

        await _inventoryRepository.SaveChangesAsync();

        return new InventoryResponseDto
        {
            ProductId = inventory.ProductId,
            AvailableQuantity = inventory.AvailableQuantity,
            ReservedQuantity = inventory.ReservedQuantity
        };
    }
    public async Task<InventoryResponseDto> DeductReservedStockAsync(DeductStockRequestDto request)
    {
        var inventory =
            await _inventoryRepository
                .GetByProductIdAsync(request.ProductId);

        if (inventory is null)
        {
            throw new NotFoundException(
                $"Inventory not found for product {request.ProductId}");
        }

        if (inventory.ReservedQuantity < request.Quantity)
        {
            throw new BadRequestException(
                "Reserved quantity is insufficient");
        }

        inventory.ReservedQuantity -= request.Quantity;
        inventory.LastUpdated = DateTime.UtcNow;

        await _inventoryRepository.UpdateAsync(inventory);

        await _inventoryRepository.SaveChangesAsync();

        return new InventoryResponseDto
        {
            ProductId = inventory.ProductId,
            AvailableQuantity = inventory.AvailableQuantity,
            ReservedQuantity = inventory.ReservedQuantity
        };
    }
}