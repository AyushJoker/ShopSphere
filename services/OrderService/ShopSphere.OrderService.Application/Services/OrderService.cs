using Microsoft.Extensions.Logging;
using ShopSphere.OrderService.Application.DTOs;
using ShopSphere.OrderService.Application.Interfaces;
using ShopSphere.OrderService.Domain.Entities;
using ShopSphere.OrderService.Domain.Enums;
using System.Security.Cryptography;

namespace ShopSphere.OrderService.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IProductServiceClient _productServiceClient;

    private readonly IInventoryServiceClient _inventoryServiceClient;

    private readonly ILogger<OrderService> _logger;

    public OrderService(
     IOrderRepository orderRepository,
     IProductServiceClient productServiceClient,
     IInventoryServiceClient inventoryServiceClient,
     ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _productServiceClient = productServiceClient;
        _inventoryServiceClient = inventoryServiceClient;
        _logger = logger;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(Guid userId,CreateOrderRequestDto request)
    {
        var reservedItems =new List<(Guid ProductId, int Quantity)>();

        try
        {
            var orderNumber =
                $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";

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
                var product = await _productServiceClient
                        .GetProductAsync(item.ProductId);

                if (product is null)
                {
                    throw new ProductNotFoundException(item.ProductId);
                }

                await _inventoryServiceClient
                    .ReserveStockAsync(
                        item.ProductId,
                        item.Quantity);

                reservedItems.Add(
                    (item.ProductId, item.Quantity));

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),

                    OrderId = order.Id,

                    ProductId = product.Id,

                    ProductName = product.Name,

                    UnitPrice = product.Price,

                    Quantity = item.Quantity,

                    SubTotal = product.Price * item.Quantity
                };

                order.Items.Add(orderItem);
            }

            order.TotalAmount =
                order.Items.Sum(x => x.SubTotal);

            await _orderRepository.AddAsync(order);

            await _orderRepository.SaveChangesAsync();

            _logger.LogInformation(
                "Order {OrderId} created successfully for User {UserId}",
                order.Id,
                userId);

            return await GetByIdAsync(order.Id)
                   ?? throw new InvalidOperationException(
                       "Order creation failed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to create order for User {UserId}. Starting inventory compensation.",
                userId);

            foreach (var reservedItem in reservedItems)
            {
                try
                {
                    await _inventoryServiceClient
                        .ReleaseStockAsync(
                            reservedItem.ProductId,
                            reservedItem.Quantity);

                    _logger.LogInformation(
                        "Released reserved inventory. ProductId: {ProductId}, Quantity: {Quantity}",
                        reservedItem.ProductId,
                        reservedItem.Quantity);
                }
                catch (Exception releaseEx)
                {
                    _logger.LogError(
                        releaseEx,
                        "Failed to release inventory. ProductId: {ProductId}, Quantity: {Quantity}",
                        reservedItem.ProductId,
                        reservedItem.Quantity);
                }
            }

            throw;
        }
    }
    public async Task UpdateOrderStatusAsync(Guid orderId,OrderStatus status)
    {
        var order =
            await _orderRepository
                .GetByIdAsync(orderId);

        if (order is null)
        {
            throw new OrderNotFoundException(orderId);
        }

        order.Status = status;

        await _orderRepository.UpdateAsync(order);

        await _orderRepository.SaveChangesAsync();
    }
    public async Task<OrderResponseDto?> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order is null)
        {
            throw new OrderNotFoundException(id);
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
            throw new OrderNotFoundException(orderId);
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