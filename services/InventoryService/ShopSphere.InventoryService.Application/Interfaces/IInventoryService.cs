using ShopSphere.InventoryService.Application.DTOs;

namespace ShopSphere.InventoryService.Application.Interfaces;

public interface IInventoryService
{
    Task<InventoryResponseDto> AddStockAsync(AddStockRequestDto request);

    Task<InventoryResponseDto?> GetInventoryAsync(Guid productId);
}