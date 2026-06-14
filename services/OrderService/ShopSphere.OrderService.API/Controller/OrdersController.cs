using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.OrderService.Application.DTOs;
using ShopSphere.OrderService.Application.Interfaces;
using System.Security.Claims;

namespace ShopSphere.OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(
        IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequestDto request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        //var userId =
        //    Guid.Parse(
        //        "11111111-1111-1111-1111-111111111111");

        var order = await _orderService.CreateOrderAsync( userId, request);

        return CreatedAtAction(nameof(GetById),
            new { id = order.Id },
            order);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order =await _orderService.GetByIdAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [Authorize]
    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var orders =await _orderService
                .GetMyOrdersAsync(userId);

        return Ok(orders);
    }

    [Authorize]
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _orderService.CancelOrderAsync(id,userId);

        if (!result)
        {
            return BadRequest( "Order cannot be cancelled.");
        }

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id,UpdateOrderStatusRequestDto request)
    {
        await _orderService
            .UpdateOrderStatusAsync(
                id,
                request.Status);

        return NoContent();
    }
}