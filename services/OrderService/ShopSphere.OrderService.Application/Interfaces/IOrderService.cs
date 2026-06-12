using ShopSphere.OrderService.Application.DTOs;
using ShopSphere.OrderService.Domain.Enums;

namespace ShopSphere.OrderService.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(Guid userId,CreateOrderRequestDto request);

    Task<OrderResponseDto?> GetByIdAsync(Guid id);

    Task<List<OrderResponseDto>> GetMyOrdersAsync(Guid userId);

    Task<bool> CancelOrderAsync(Guid orderId,Guid userId);

    Task UpdateOrderStatusAsync(Guid orderId,OrderStatus status);
}