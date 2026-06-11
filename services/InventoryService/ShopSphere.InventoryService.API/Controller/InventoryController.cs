using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.InventoryService.Application.DTOs;
using ShopSphere.InventoryService.Application.Interfaces;

namespace ShopSphere.InventoryService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("add-stock")]
    public async Task<IActionResult> AddStock(AddStockRequestDto request)
    {
        var response =
            await _inventoryService
                .AddStockAsync(request);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetInventory(Guid productId)
    {
        var inventory =
            await _inventoryService
                .GetInventoryAsync(productId);

        if (inventory is null)
        {
            return NotFound();
        }

        return Ok(inventory);
    }
}