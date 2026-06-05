using ShopSphere.OrderService.Application.DTOs;

namespace ShopSphere.OrderService.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(Guid userId,CreateOrderRequestDto request);

    Task<OrderResponseDto?> GetByIdAsync(Guid id);

    Task<List<OrderResponseDto>> GetMyOrdersAsync(Guid userId);

    Task<bool> CancelOrderAsync(Guid orderId,Guid userId);
}