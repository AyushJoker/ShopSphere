using ShopSphere.OrderService.Application.DTOs;
using ShopSphere.OrderService.Application.Interfaces;
using ShopSphere.OrderService.Domain.Entities;
using ShopSphere.OrderService.Domain.Enums;

namespace ShopSphere.OrderService.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(
        IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(Guid userId,CreateOrderRequestDto request)
    {
        var orderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";

        var order = new Order
        {
            Id = Guid.NewGuid(),

            OrderNumber = orderNumber,

            UserId = userId,

            OrderDate = DateTime.UtcNow,

            Status = OrderStatus.Pending
        };
        foreach (var item in request.Items)
        {
            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,

                ProductId = item.ProductId,

                ProductName = "Sample Product",

                UnitPrice = 100,

                Quantity = item.Quantity,

                SubTotal = 100 * item.Quantity
            };

            order.Items.Add(orderItem);
        }

        order.TotalAmount =
            order.Items.Sum(x => x.SubTotal);

        await _orderRepository.AddAsync(order);

        await _orderRepository.SaveChangesAsync();

        return await GetByIdAsync(order.Id)
               ?? throw new InvalidOperationException(
                   "Order creation failed.");
    }
    public async Task<OrderResponseDto?> GetByIdAsync(Guid id)
    {
        var order =
            await _orderRepository.GetByIdAsync(id);

        if (order is null)
        {
            return null;
        }

        return new OrderResponseDto
        {
            Id = order.Id,

            UserId = order.UserId,

            OrderNumber = order.OrderNumber,

            OrderDate = order.OrderDate,

            TotalAmount = order.TotalAmount,

            Status = order.Status,

            Items = order.Items
                .Select(item =>
                    new OrderItemResponseDto
                    {
                        ProductId = item.ProductId,

                        ProductName = item.ProductName,

                        UnitPrice = item.UnitPrice,

                        Quantity = item.Quantity,

                        SubTotal = item.SubTotal
                    })
                .ToList()
        };
    }
    public async Task<bool> CancelOrderAsync(Guid orderId,Guid userId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order is null)
        {
            return false;
        }

        if (order.UserId != userId)
        {
            return false;
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            return false;
        }

        if (order.Status == OrderStatus.Shipped ||
            order.Status == OrderStatus.Delivered)
        {
            return false;
        }

        order.Status = OrderStatus.Cancelled;

        await _orderRepository.SaveChangesAsync();

        return true;
    }
    public async Task<List<OrderResponseDto>>GetMyOrdersAsync(Guid userId)
    {
        var orders = await _orderRepository
                .GetByUserIdAsync(userId);

        return orders.Select(order =>
            new OrderResponseDto
            {
                Id = order.Id,

                OrderNumber = order.OrderNumber,

                UserId = order.UserId,

                OrderDate = order.OrderDate,

                TotalAmount = order.TotalAmount,

                Status = order.Status,

                Items = order.Items
                    .Select(item =>
                        new OrderItemResponseDto
                        {
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity,
                            SubTotal = item.SubTotal
                        })
                    .ToList()
            })
            .ToList();
    }
}