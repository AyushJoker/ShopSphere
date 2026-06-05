using ShopSphere.OrderService.Domain.Entities;

namespace ShopSphere.OrderService.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task<Order?> GetByIdAsync(Guid id);

    Task<List<Order>> GetByUserIdAsync(Guid userId);

    Task SaveChangesAsync();
}